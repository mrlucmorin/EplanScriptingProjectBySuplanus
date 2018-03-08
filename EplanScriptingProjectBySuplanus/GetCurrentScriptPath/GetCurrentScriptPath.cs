/* Usage
    private static string GetCurrentScriptPath(string scriptName)
    {
        string value = null;
        ActionCallingContext actionCallingContext = new ActionCallingContext();
        actionCallingContext.AddParameter("scriptName", scriptName);
        new CommandLineInterpreter().Execute("GetCurrentScriptPath", actionCallingContext);
        actionCallingContext.GetParameter("value", ref value);
        return value;
    }
*/

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

namespace EplanScriptingProjectBySuplanus.GetCurrentScriptPath
{
    class GetCurrentScriptPath
    {
        [DeclareAction("GetCurrentScriptPath")]
        public void Action(string scriptName, out string value)
        {
            Settings settings = new Settings();
            string scriptPath = string.Empty;

            // If script is loaded
            var settingsUrlScripts = "STATION.EplanEplApiScriptGui.Scripts";
            int countOfScripts = settings.GetCountOfValues(settingsUrlScripts);
            for (int i = 0; i < countOfScripts; i++)
            {
                scriptPath = settings.GetStringSetting(settingsUrlScripts, i);
                if (scriptPath.EndsWith(@"\" + scriptName))
                {
                    value = Path.GetDirectoryName(scriptPath);
                    return;
                }
            }

            // If script is executed
            if (settings.GetStringSetting("USER.FileSelection.1.PermamentSelection.Files.file1", 0) == scriptName)
            {
                value = settings.GetStringSetting("USER.FileSelection.1.PermamentSelection.FolderName", 0);
                return;
            }

            // Not found
            value = null;
            return;
        }
    }
}