using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace TONK_BONK;

public class TONK_BONK : PhysicsGame
{
    public Image TonkRuumis = LoadImage("TONK_BONK_chassis3");
    private PhysicsObject tonk;
    private Vector TONK_position = new Vector(20, 20);
    public override void Begin()
    { 
        LuoTONK(); 
        Luokentta();
        AsetaNappeimet();
        
    }

    void Luokentta()
    {Level.BackgroundColor = Color.Gray;
    
    }

    void LuoTONK()
    {
        tonk = new PhysicsObject( 40, 20);
        tonk.Shape = Shape.Triangle;
        tonk.Angle = Angle.FromDegrees(0.0); 
        tonk.Color = Color.JungleGreen;
        tonk.Image = TonkRuumis;
        Add(tonk);
    }

    void AsetaNappeimet()
    { 
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.W, ButtonState.Down, Liiku, "Liikuta Tankki", 100.0);
        Keyboard.Listen(Key.W, ButtonState.Released, Liiku, "Liikuta Tankki", 0.0 );
        Keyboard.Listen(Key.S, ButtonState.Down, Liiku, "Liikuta Tankki", -60.0);
        Keyboard.Listen(Key.S, ButtonState.Released, Liiku, "Liikuta Tankki", 0.0 );
        Keyboard.Listen(Key.A, ButtonState.Released, TonkKulma, "Liikuta Tankki", 0.0 );
        Keyboard.Listen(Key.A, ButtonState.Down,TonkKulma , "Liikuta Tankki", 1.5);
        Keyboard.Listen(Key.D, ButtonState.Released, TonkKulma, "Liikuta Tankki", 0.0 );
        Keyboard.Listen(Key.D, ButtonState.Down,TonkKulma , "Liikuta Tankki", -1.5);}   
     void Liiku (double kerroin) 
     {
         tonk.Velocity = tonk.Angle.GetVector() * kerroin;
     }

     void TonkKulma(double nopeus )
     {
         tonk.AngularVelocity = nopeus;
         
     }
     
     
     
}