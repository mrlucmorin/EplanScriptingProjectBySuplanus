﻿//
//  Dieses Skript fügt einen Menüpunkt "Seite drucken" im Kontextmenü des 
//  grafischen Editors hinzu. Die Seite wird über den vom Benutzer als
//  Standard definierten Drucker ausgegeben.
//
//  Christian Klasen  
//
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


public class QuickprintContextMenu
{
    [DeclareRegister]
    public void Register()
    {
        MessageBox.Show("Script geladen.");

        return;
    }

    [DeclareUnregister]
    public void UnRegister()
    {
        MessageBox.Show("Script entladen.");

        return;
    }


    [DeclareAction("MenuAction")]
    public void ActionFunction()
    {
        CommandLineInterpreter oCLI = new CommandLineInterpreter();
        ActionCallingContext acc = new ActionCallingContext();

        string strPages = string.Empty;

        acc.AddParameter("TYPE", "PAGES");

        oCLI.Execute("selectionset", acc);

        acc.GetParameter("PAGES", ref strPages);
    
        acc.AddParameter("PAGENAME", strPages);

        oCLI.Execute("print", acc);

        //MessageBox.Show("Seite gedruckt");
        return;
    }

    [DeclareMenu]
    public void MenuFunction()
    {
        Eplan.EplApi.Gui.ContextMenu oMenu =
            new Eplan.EplApi.Gui.ContextMenu();

        Eplan.EplApi.Gui.ContextMenuLocation oLocation =
            new Eplan.EplApi.Gui.ContextMenuLocation(
                "Editor",
                "Ged"
                );

        oMenu.AddMenuItem(
            oLocation,
            "Seite drucken",
            "MenuAction",
            true,
            false
            );

        return;
    }

}




