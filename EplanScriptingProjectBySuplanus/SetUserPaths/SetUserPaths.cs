/*
The following compiler directive is necessary to enable editing scripts
within Visual Studio.

It requires that the "Conditional compilation symbol" SCRIPTENV be defined 
in the Visual Studio project properties

This is because EPLAN's internal scripting engine already adds "using directives"
when you load the script in EPLAN. Having them twice would cause errors.
*/

#if SCRIPTENV
using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Scripting;
using Eplan.EplApi.Base;
using Eplan.EplApi.Gui;
#endif

/*
On the other hand, some namespaces are not automatically added by EPLAN when
you load a script. Those have to be outside of the previous conditional compiler directive
*/

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;


public class SetUserPaths
{
    [DeclareAction("SetUserPaths")]
    public void Function(string SchemeName)
    {
        Settings oSettings = new Settings();
        oSettings.SetStringSetting("USER.ModalDialogs.PathsScheme.LastUsed", SchemeName, 0);

        SettingNode Sn = new SettingNode("USER.ModalDialogs.PathsScheme." + SchemeName + ".Data");
        System.Collections.Specialized.StringCollection Sc = new System.Collections.Specialized.StringCollection();
        Sn.GetListOfSettings(ref Sc, false);
        foreach (string s in Sc)
        {
            string sValue = oSettings.GetStringSetting("USER.ModalDialogs.PathsScheme." + SchemeName + ".Data." + s, 0);
            switch (s)
            {
                case "ExternalDocuments":
                case "Scheme":
                case "Scripts":
                case "XML":
                    oSettings.SetStringSetting("USER.SYSTEM.Pathnames." + s, sValue, 0);
                    break;

                default:
                    oSettings.SetStringSetting("USER.TrDMProject.Masterdata.Pathnames." + s, sValue, 0);
                    break;
            }
        }
    }
}
