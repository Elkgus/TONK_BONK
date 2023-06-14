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
    public Image tonkTorniKuva = LoadImage("TONK_torni2.png");
    private PhysicsObject tonk;
    private PhysicsObject tonkTorni;
    private Vector TONK_position = new Vector(80, 40);
    public override void Begin()
    { 
        LuoTONK(); 
        Luokentta();
        AsetaNappeimet();
        
    }

    void Luokentta()
    {Level.BackgroundColor = Color.Blue;
    
    }

    void LuoTONK()
    {
        tonk = new PhysicsObject( 160, 80);
        tonk.Angle = Angle.FromDegrees(0.0);
        tonk.Image = TonkRuumis;
        Add(tonk);
         
        tonkTorni = new PhysicsObject(240, 60);
        tonkTorni.Image = tonkTorniKuva;
        Add(tonkTorni,1);
        tonkTorni.IgnoresCollisionResponse = true;
        //WheelJoint liitos = new WheelJoint(tonk, tonkTorni);
        //Add(liitos);
        //AxleJoint liitos = new AxleJoint(tonkTorni, tonk, new Vector(-10,0));
        //Add(liitos);
        //liitos.Softness = 0.5;
        tonk.Add(tonkTorni);
        //tonk.Position = tonk.Position;
        //tonkTorni.Angle = tonkTorni.Angle*7;
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
        Keyboard.Listen(Key.D, ButtonState.Down,TonkKulma , "Liikuta Tankki", -1.5);
        Keyboard.Listen(Key.Left, ButtonState.Down, TonkTornikulma, "Liikuta Tykki", 1.0);
        Keyboard.Listen(Key.Left, ButtonState.Released, TonkTornikulma, "Liikuta Tykki", 0.0);
    }   
   
    void Liiku (double kerroin) 
     {
         tonk.Velocity = tonk.Angle.GetVector() * kerroin;
     }

     void TonkKulma(double nopeus )
     {
         tonk.AngularVelocity = nopeus;
         
     }
     
     void TonkTornikulma(double nopeus )
     {
         tonkTorni.AngularVelocity = nopeus;
         
     }

     void Ampuminen()
     {
         
         
     }
}