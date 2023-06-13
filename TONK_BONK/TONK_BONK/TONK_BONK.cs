using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace TONK_BONK;

public class TONK_BONK : PhysicsGame
{
    public override void Begin()
    {
    //
    Luokentta();
    PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    void Luokentta()
    {
        PhysicsObject TONK = new PhysicsObject( 40, 20);
     TONK.Shape = Shape.Triangle;
     TONK.Color = Color.JungleGreen;
     TONK.Position = new Vector(20, 20);
     Add(TONK);
    }
        
    }