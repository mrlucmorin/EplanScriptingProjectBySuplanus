// PlaceHolderTranslateAction.cs
//
// Erweitert das Kontextmenü vom Platzhalterobjekt (Reiter Werte) um den Menüpunkt "Übersetzen"
// und um den Menüpunkt "Übersetzungen entfernen"
//
// Copyright by Frank Schöneck, 2015
// letzte Änderung:
// V1.0.0, 23.10.2015, Frank Schöneck, Projektbeginn
//
// für Eplan Electric P8, ab V2.5
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


namespace EplanScriptingProjectBySuplanus.PlaceHolderTranslateAction
{
    public class PlaceHolderTranslate
    {
        [DeclareMenu]
        public void PlaceHolderTranslateContextMenu()
        {
            //Context-Menüeintrag (hier im Platzhalterobjekt)
            Eplan.EplApi.Gui.ContextMenu oContextMenu = new Eplan.EplApi.Gui.ContextMenu();
            Eplan.EplApi.Gui.ContextMenuLocation oContextMenuLocation = new Eplan.EplApi.Gui.ContextMenuLocation("PlaceHolder", "1004");
            oContextMenu.AddMenuItem(oContextMenuLocation, "Übersetzen", "PlaceHolderTranslateAction", false, false);
            oContextMenu.AddMenuItem(oContextMenuLocation, "Übersetzungen entfernen", "PlaceHolderTranslateDeleteAction", false, false);
        }

        [DeclareAction("PlaceHolderTranslateAction")]
        public void PlaceHolderTranslate_Action()
        {
            //Übersetzen
            new CommandLineInterpreter().Execute("EnfTranslateEditAction");
        }

        [DeclareAction("PlaceHolderTranslateDeleteAction")]
        public void PlaceHolderTranslateDelete_Action()
        {
            //Übersetzungen entfernen
            new CommandLineInterpreter().Execute("EnfDeleteEditTranslationsAction");
        }

    }
}
