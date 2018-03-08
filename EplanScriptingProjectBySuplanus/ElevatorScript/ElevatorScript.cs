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

class Class
{
    [Start]
    public void Function()
    {
        Console.Beep(420, 200);
        Console.Beep(400, 200);
        Console.Beep(420, 200);
        Console.Beep(400, 200);
        Console.Beep(420, 200);
        Console.Beep(315, 200);
        Console.Beep(370, 200);
        Console.Beep(335, 200);
        Console.Beep(282, 600);
        Console.Beep(180, 200);
        Console.Beep(215, 200);
        Console.Beep(282, 200);
        Console.Beep(315, 600);
        Console.Beep(213, 200);
        Console.Beep(262, 200);
        Console.Beep(315, 200);
        Console.Beep(335, 600);
        Console.Beep(213, 200);
        Console.Beep(420, 200);
        Console.Beep(400, 200);
        Console.Beep(420, 200);
        Console.Beep(400, 200);
        Console.Beep(420, 200);
        Console.Beep(315, 200);
        Console.Beep(370, 200);
        Console.Beep(335, 200);
        Console.Beep(282, 600);
        Console.Beep(180, 200);
        Console.Beep(215, 200);
        Console.Beep(282, 200);
        Console.Beep(315, 600);
        Console.Beep(213, 200);
        Console.Beep(330, 200);
        Console.Beep(315, 200);
        Console.Beep(282, 600);

        return;
    }

}