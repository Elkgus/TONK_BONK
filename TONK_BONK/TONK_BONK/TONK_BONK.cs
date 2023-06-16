using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace TONK_BONK;
// versio 1.0
public class TONK_BONK : PhysicsGame
{
    public Image TykkiKuula = LoadImage("panos.pmg");
    public Image TonkRuumis = LoadImage("TONK_BONK_chassis3.png");
    public Image tonkTorniKuva = LoadImage("TONK_torni2.png");
    public Image panssariKuva = LoadImage("Panssar_chassis3.png");
    public Image panssariTorniKuva = LoadImage("Panssar_torni2.png");
    public static Cannon Tykki;
    public double ruudunKoko = 5;
    public PhysicsObject tonk;
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
        TileMap tiles = TileMap.FromFile("Kentta1");
        tiles.SetTileMethod('w',Luoseina);
        tiles.Execute(40,40);
    }

    void LuoTONK()
    {
        tonk = new PhysicsObject( 160, 80);
        tonk.Angle = Angle.FromDegrees(0.0);
        tonk.Image = TonkRuumis;
        tonk.Mass = 100;
        tonk.AddCollisionIgnoreGroup(1);
        Add(tonk);
        tonk.Y = -300; 
        
        tonkTorni = new PhysicsObject(240, 60);
        tonkTorni.Image = tonkTorniKuva;
        tonkTorni.Mass = 1;
        tonkTorni.AddCollisionIgnoreGroup(1);
        tonk.IgnoresPhysicsLogics = true;
        tonkTorni.IgnoresCollisionResponse = true;
        tonkTorni.Y = -300;
        
        Add(tonkTorni,1);
        torninLiitos = new AxleJoint(tonk, tonkTorni, new Vector(10,-0));
        torninLiitos.DampingRatio = 25;
        torninLiitos.Softness = 90;
        Add(torninLiitos);
        Tykki = new Cannon(40,20);
        Tykki.X =-80 ;
        Tykki.IsVisible = false;
        Tykki.Power.Value = 999999999;
        Tykki.Y = -300;
        tonkTorni.Add(Tykki);
    }

    void LuopanssariPahis()
    {
       PhysicsObject vihuRuumis = new PhysicsObject( 160, 80);
        vihuRuumis.Angle = Angle.FromDegrees(0.0);
        vihuRuumis.Image = panssariKuva;
        vihuRuumis.Mass = 100;
        vihuRuumis.AddCollisionIgnoreGroup(1);
        Add(vihuRuumis);
        vihuRuumis.IgnoresPhysicsLogics = true;
        vihuRuumis.Y = 300; 
        
        PhysicsObject panssariTorni = new PhysicsObject(240, 60);
        panssariTorni.Image = tonkTorniKuva;
        panssariTorni.Mass = 1;
        panssariTorni.AddCollisionIgnoreGroup(1);
        panssariTorni.Y = 300;
        
        Add(panssariTorni,1);
        AxleJoint panssariTorniLiitos = new AxleJoint(tonk, tonkTorni, new Vector(10,-0));
        panssariTorniLiitos.DampingRatio = 25;
        panssariTorniLiitos.Softness = 90;
        Add(panssariTorniLiitos);
        Cannon panssariTykki = new Cannon(40,20);
        panssariTykki.X =-80 ;
        panssariTykki.IsVisible = false;
        panssariTykki.Power.Value = 999999999;
        panssariTykki.Y = 300;
        panssariTorni.Add(panssariTykki);
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
             kuula.Image = TykkiKuula;
             kuula.Width *=1.7 ;
             kuula.Height *=1; ;
             kuula.Tag = "panos";
             kuula.MaximumLifetime = TimeSpan.FromSeconds(2.0);
         }
     }

     void Luoseina(Vector paikka, double leveys, double korkeus)
     {
         PhysicsObject seina = new PhysicsObject(leveys ,korkeus);
         seina.MakeStatic();
         seina.Position = paikka;
         AddCollisionHandler(seina,"panos", CollisionHandler.ExplodeBoth(100.5,true) );
         Add(seina);
     }

     
}