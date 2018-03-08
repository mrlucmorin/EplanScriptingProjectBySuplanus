' GedActionToggleConstructionMode + GedToggleObjectSnapAction, Version 1.0.0, vom 27.07.2012
'
' Copyright by Frank Schöneck, 2012
'
' für Eplan Electric P8, ab V2.1.6
'
'


'The following compiler directive Is necessary To enable editing scripts
'within Visual Studio.

'It requires that the "Conditional compilation symbol" SCRIPTENV be defined 
'in the Visual Studio project properties

'This Is because EPLAN's internal scripting engine already adds "using directives"
'when you load the script in EPLAN. Having them twice would cause errors.


#If SCRIPTENV Then
Imports Eplan.EplApi.ApplicationFramework;
Imports Eplan.EplApi.Scripting;
Imports Eplan.EplApi.Base;
Imports Eplan.EplApi.Gui;
#End If


'On the other hand, some namespaces are Not automatically added by EPLAN when
'you load a script. Those have To be outside Of the previous conditional compiler directive


Imports System;
Imports System.IO;
Imports System.Windows.Forms;
Imports System.Collections.Generic;
Imports System.Linq;

Public Class GedToggleObjectAction_Class

    <DeclareEventHandler("onActionStart.String.XGedActionToggleConstructionMode")>
    Public Function GedActionToggleConstructionMode(ByVal iEventParameter As IEventParameter) As Long
        Dim CLI As New CommandLineInterpreter()
        CLI.Execute("XGedToggleObjectSnapAction") 'Objektfang
    End Function

End Class
