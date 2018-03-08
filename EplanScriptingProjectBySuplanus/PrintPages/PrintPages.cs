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


public class PrintPages
{
    [DeclareAction("PrintPages")]
    public void PrintPagesVoid()
    {
        CommandLineInterpreter oCLI = new CommandLineInterpreter();
        ActionCallingContext acc = new ActionCallingContext();

        string strPages = string.Empty;
        acc.AddParameter("TYPE", "PAGES");
        oCLI.Execute("selectionset", acc);
        acc.GetParameter("PAGES", ref strPages);

        Progress oProgress = new Progress("SimpleProgress");
        oProgress.SetAllowCancel(true);
        oProgress.SetAskOnCancel(true);
        oProgress.SetNeededSteps(3);
        oProgress.SetTitle("Drucken");
        oProgress.ShowImmediately();


        foreach (string Page in strPages.Split(';'))
        {
            if (!oProgress.Canceled())
            {
                acc.AddParameter("PAGENAME", Page);
                oCLI.Execute("print", acc);
            }
            else
            {
                break;
            }
        }
        oProgress.EndPart(true);

        return;
    }

    [DeclareMenu]
    public void MenuFunction()
    {
        Eplan.EplApi.Gui.ContextMenu oMenu =
            new Eplan.EplApi.Gui.ContextMenu();

        Eplan.EplApi.Gui.ContextMenuLocation oLocation =
            new Eplan.EplApi.Gui.ContextMenuLocation(
                "PmPageObjectTreeDialog",
                "1007"
                );

        oMenu.AddMenuItem(
            oLocation,
            "Seite(n) drucken",
            "PrintPages",
            true,
            false
            );

        return;
    }

}




