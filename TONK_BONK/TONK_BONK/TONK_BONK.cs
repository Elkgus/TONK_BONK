using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace TONK_BONK;

public class TONK_BONK : PhysicsGame
{
    public Image TykkiKuula = LoadImage("panos");
    public Image TonkRuumis = LoadImage("TONK_BONK_chassis3");
    public Image tonkTorniKuva = LoadImage("TONK_torni2.png");
    public static Cannon Tykki;
    private PhysicsObject tonk;
    private PhysicsObject tonkTorni;
    private AxleJoint torninLiitos;
    private Vector TONK_position = new Vector(80, 40);
    public override void Begin()
    { 
        LuoTONK(); 
        Luokentta();
        AsetaNappeimet();
        
    }

    void Luokentta()
    { 
        Level.BackgroundColor = Color.Blue;
        Level.CreateBorders();
    }

    void LuoTONK()
    {
        tonk = new PhysicsObject( 160, 80);
        tonk.Angle = Angle.FromDegrees(0.0);
        tonk.Image = TonkRuumis;
        tonk.Mass = 100;
        tonk.AddCollisionIgnoreGroup(1);
        Add(tonk);
         
        tonkTorni = new PhysicsObject(240, 60);
        tonkTorni.Image = tonkTorniKuva;
        tonkTorni.Mass = 1;
        tonkTorni.AddCollisionIgnoreGroup(1);
        
        Add(tonkTorni,1);
        torninLiitos = new AxleJoint(tonk, tonkTorni, new Vector(10,0));
        torninLiitos.DampingRatio = 25;
        torninLiitos.Softness = 90;
        Add(torninLiitos);
        Tykki = new Cannon(40,20);
        Tykki.X = 0;
        Tykki.IsVisible = false;
        tonkTorni.Add(Tykki);
    }

    void AsetaNappeimet()
    { 
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.W, ButtonState.Down, Liiku, "Liikuta Tankki", 100.0);
        Keyboard.Listen(Key.W, ButtonState.Released, Liiku, "Liikuta Tankki", 0.0 );
        Keyboard.Listen(Key.S, ButtonState.Down, Liiku, "Liikuta Tankki", -60.0);
        Keyboard.Listen(Key.S, ButtonState.Released, Liiku, "Liikuta Tankki", 0.0 );
        Keyboard.Listen(Key.A, ButtonState.Released, TonkKulma, "Liikuta Tankki", 0.0 );
        Keyboard.Listen(Key.A, ButtonState.Down,TonkKulma , "Liikuta Tankki", .7);
        Keyboard.Listen(Key.D, ButtonState.Released, TonkKulma, "Liikuta Tankki", 0.0 );
        Keyboard.Listen(Key.D, ButtonState.Down,TonkKulma , "Liikuta Tankki", -.7);
        Keyboard.Listen(Key.Left, ButtonState.Down, TonkTornikulma, "Liikuta Tykki", 1.0, true);
        Keyboard.Listen(Key.Left, ButtonState.Released, TonkTornikulma, "Liikuta Tykki", 0.0, false);
        Keyboard.Listen(Key.Right, ButtonState.Down, TonkTornikulma, "Liikuta Tykki", -1.0, true);
        Keyboard.Listen(Key.Right, ButtonState.Released, TonkTornikulma, "Liikuta Tykki", 0.0, false);
        Keyboard.Listen(Key.Space, ButtonState.Down,Ammutykki, "ampua", Tykki);
    }   
   
    void Liiku (double kerroin) 
     {
         tonk.Velocity = tonk.Angle.GetVector() * kerroin;
         if (kerroin == 0)
         {
             tonkTorni.Stop();   
         } 
     }

     void TonkKulma(double nopeus )
     {
         tonk.AngularVelocity = nopeus;
         if (nopeus == 0)
         {
             tonkTorni.Stop();   
         } 
         
     }
     
     void TonkTornikulma(double nopeus, bool canRotate )
     {
         tonkTorni.CanRotate = canRotate;
         tonkTorni.AngularVelocity = nopeus;
         if (!canRotate)
         {
          tonkTorni.Stop();   
         } 
             
     }

     void Ammutykki(Cannon ase)
     {
         PhysicsObject kuula = ase.Shoot();
         if (kuula != null)
         {
             kuula.Size *= 1;
             kuula.Image = TykkiKuula;
         }
     }
}