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


namespace ibKastl.Scripts.Test
{
    class ScriptTest
    {
        /// <summary>
        /// Start Visual Studio with the given Script
        /// </summary>
        /// <param name="scriptPath">Full Path to .cs or .vb File</param>
        /// <param name="parameter">Parameters for the Action</param>
        /// <param name="execute">Excute after EPLAN started</param>
        [DeclareAction("ScriptTest")]               
        public void Action(string scriptPath, string parameter, string execute)
        {
            // Check file
            if (!File.Exists(scriptPath))
            {
                MessageBox.Show("Scriptdatei wurde nicht gefunden:"
                                + Environment.NewLine + scriptPath + Environment.NewLine +
                                "Das Script wurde nicht geladen.",
                                "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string actionName = Path.GetFileNameWithoutExtension(scriptPath);
                CommandLineInterpreter commandLineInterpreter = new CommandLineInterpreter();

                // Unload if Action exists
                ActionCallingContext actionCallingContextUnregister = new ActionCallingContext();
                actionCallingContextUnregister.AddParameter("DontShowErrorMessage", "1");
                actionCallingContextUnregister.AddParameter("ScriptFile", scriptPath);
                commandLineInterpreter.Execute("UnregisterScript", actionCallingContextUnregister);

                // Load Script 
                ActionCallingContext actionCallingContextRegister = new ActionCallingContext();
                actionCallingContextRegister.AddParameter("ScriptFile", scriptPath);
                commandLineInterpreter.Execute("RegisterScript", actionCallingContextRegister);

                // Execute
                if (execute.ToLower().Equals("true"))
                {
                    if (!string.IsNullOrEmpty(parameter))
                    {
                        commandLineInterpreter.Execute(actionName + " " + parameter); 
                    }
                    else
                    {
                        commandLineInterpreter.Execute(actionName); 
                    }
                }
            }

        }
    }
}