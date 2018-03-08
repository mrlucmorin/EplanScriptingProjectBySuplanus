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


public class OpenProjectAndSetPartsDb
{
    [DeclareAction("OpenProjectAndSetPartsDb")]
    public void OpenProjectAndSetPartsDbVoid(string PROJECT,string DATABASE)
    {
        if (File.Exists(DATABASE))
        {
            Eplan.EplApi.Base.Settings oSettings = new Eplan.EplApi.Base.Settings();
            oSettings.SetStringSetting("USER.PartsManagementGui.Database", DATABASE, 0);
            MessageBox.Show("Eingestellte Datenbank:\n" + DATABASE, "OpenProjectAndSetPartsDb", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Datenbank nicht gefunden:\n" + DATABASE + "\n\n Es wurde keine Änderung an den Einstellungen vorgenommen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (File.Exists(PROJECT))
        {
            ActionCallingContext accProjectOpen = new ActionCallingContext();
            accProjectOpen.AddParameter("Project", PROJECT);
            new CommandLineInterpreter().Execute("ProjectOpen", accProjectOpen);
        }
        else
        {
            MessageBox.Show("Projekt nicht gefunden:\n" + PROJECT, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return;

    }

}