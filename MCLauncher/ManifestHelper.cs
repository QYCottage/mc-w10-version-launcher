using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

public static class ManifestHelper
{
    private const string SCCD_BASE64 = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4KPEN1c3RvbUNhcGFiaWxpdHlEZXNjcmlwdG9yIHhtbG5zPSJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL2FwcHgvMjAxOC9zY2NkIiB4bWxuczpzPSJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL2FwcHgvMjAxOC9zY2NkIj4KICA8Q3VzdG9tQ2FwYWJpbGl0aWVzPgogICAgPEN1c3RvbUNhcGFiaWxpdHkgTmFtZT0iTWljcm9zb2Z0LmNvcmVBcHBBY3RpdmF0aW9uXzh3ZWt5YjNkOGJid2UiPjwvQ3VzdG9tQ2FwYWJpbGl0eT4KICA8L0N1c3RvbUNhcGFiaWxpdGllcz4KICA8QXV0aG9yaXplZEVudGl0aWVzIEFsbG93QW55PSJ0cnVlIi8+CiAgPENhdGFsb2c+RkZGRjwvQ2F0YWxvZz4KPC9DdXN0b21DYXBhYmlsaXR5RGVzY3JpcHRvcj4=";

    public static bool ModifyManifest(string directory)
    {
        var manifestPath = Path.Combine(directory, "AppxManifest.xml");
        if (!File.Exists(manifestPath))
            return false;

        XDocument doc = XDocument.Load(manifestPath, LoadOptions.PreserveWhitespace);
        XNamespace ns = "http://schemas.microsoft.com/appx/manifest/foundation/windows10";
        XNamespace rescap = "http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities";
        XNamespace uap4 = "http://schemas.microsoft.com/appx/manifest/uap/windows10/4";
        XNamespace uap10 = "http://schemas.microsoft.com/appx/manifest/uap/windows10/10";

        var packageElem = doc.Root;
        if (packageElem == null)
            return false;

        var ign = packageElem.Attribute("IgnorableNamespaces");
        if (ign == null)
            packageElem.SetAttributeValue("IgnorableNamespaces", "uap uap4 uap10 rescap");
        else
        {
            var str = ign.Value;
            foreach (var n in new[] { "uap", "uap4", "uap10", "rescap" })
                if (!str.Contains(n)) str += " " + n;
            packageElem.SetAttributeValue("IgnorableNamespaces", str.Trim());
        }
        packageElem.SetAttributeValue(XNamespace.Xmlns + "rescap", rescap.NamespaceName);
        packageElem.SetAttributeValue(XNamespace.Xmlns + "uap4", uap4.NamespaceName);
        packageElem.SetAttributeValue(XNamespace.Xmlns + "uap10", uap10.NamespaceName);

        var applications = packageElem.Element(ns + "Applications");
        if (applications != null)
        {
            var application = applications.Element(ns + "Application");
            if (application != null)
            {
                application.SetAttributeValue(uap10 + "TrustLevel", "mediumIL");
            }
        }

        var cap = packageElem.Element(ns + "Capabilities");
        if (cap != null)
        {
            cap.Elements(rescap + "Capability").Remove();
            cap.Elements(uap4 + "CustomCapability").Remove();
            var devCaps = cap.Elements(ns + "DeviceCapability").ToList();
            foreach (var d in devCaps)
                d.Remove();

   
            cap.Add(new XElement(rescap + "Capability", new XAttribute("Name", "runFullTrust")));
            cap.Add(new XElement(uap4 + "CustomCapability", new XAttribute("Name", "Microsoft.coreAppActivation_8wekyb3d8bbwe")));

            foreach (var d in devCaps)
                cap.Add(d);
            if (!devCaps.Any())
                cap.Add(new XElement(ns + "DeviceCapability", new XAttribute("Name", "internetClient"))); // or 你需要的
        }

        doc.Save(manifestPath, SaveOptions.DisableFormatting);


        var sccdPath = Path.Combine(directory, "CustomCapability.SCCD");
        try
        {
            File.WriteAllBytes(sccdPath, Convert.FromBase64String(SCCD_BASE64));
        }
        catch
        {
            return false;
        }
        return true;
    }
}