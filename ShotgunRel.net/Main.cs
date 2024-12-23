using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using GTA;

namespace ShotgunRel
{
    public class Main : Script
    {
        GTA.Native.Pointer shotrel = 0.0;
        int ClipAmmo = 0;
        int TotalAmmo = 0;
        bool SpacePressed = false;
        bool ReloadStart = false;
        bool LastShot = false;

        public Main()
        {
            KeyDown += ShotgunRel_KeyDown;
            this.Tick += new EventHandler(this.ScriptCommunicationExample2_Tick);
            GTA.Native.Function.Call("REQUEST_ANIMS", "gun@shotgun");
            GTA.Native.Function.Call("REQUEST_ANIMS", "gun@baretta");

        }
        private void ScriptCommunicationExample2_Tick(object sender, EventArgs e)
        {
            if (Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll)
            {
                if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload") && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch") && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload") && !GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch"))
                {
                    if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                    {
                        ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                        TotalAmmo = Player.Character.Weapons.Current.Ammo;
                    }
                    if (Player.Character.Weapons.Current.AmmoInClip == 1 && GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                    {
                        if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "fire"))
                        {
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "fire", shotrel);
                            if (shotrel > 0.6 && shotrel < 0.64)
                            {
                                LastShot = true;
                            }
                            if (shotrel > 0.67 && shotrel < 0.72)
                            {
                                LastShot = false;
                            }
                        }
                        else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "fire_crouch"))
                        {
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "fire_crouch", shotrel);
                            if (shotrel > 0.6 && shotrel < 0.64)
                            {
                                LastShot = true;
                            }
                            if (shotrel > 0.67 && shotrel < 0.72)
                            {
                                LastShot = false;
                            }
                        }
                        else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "fire"))
                        {
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "fire", shotrel);
                            if (shotrel > 0.85 && shotrel < 0.88)
                            {
                                LastShot = true;
                            }
                            if (shotrel > 0.91 && shotrel < 0.94)
                            {
                                LastShot = false;
                            }
                        }
                        else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "fire_crouch"))
                        {
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "fire_crouch", shotrel);
                            if (shotrel > 0.85 && shotrel < 0.88)
                            {
                                LastShot = true;
                            }
                            if (shotrel > 0.91 && shotrel < 0.94)
                            {
                                LastShot = false;
                            }
                        }
                    }
                }
                else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload"))
                {
                    if (Player.Character.Weapons.Current.AmmoInClip > ClipAmmo)
                    {
                        Player.Character.Weapons.Current.AmmoInClip = ClipAmmo;
                        Player.Character.Weapons.Current.Ammo = TotalAmmo;
                        if (LastShot == true)
                        {
                            Player.Character.Weapons.Current.Ammo -= 1;
                        }
                    }
                    if ((Player.Character.Weapons.Current.AmmoInClip == 0 || LastShot == true) && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
                    {
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                        if (shotrel <= 0.4)
                        {
                            GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", 0.5);
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                        }
                        if (shotrel <= 0.85 && shotrel > 0.45)
                        {
                            return;
                        }
                        if (shotrel >= 0.85 && shotrel < 0.9)
                        {
                            Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                            Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                            if (LastShot == false)
                            {
                                Player.Character.Weapons.Current.Ammo -= 1;
                            }
                            ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                            TotalAmmo = Player.Character.Weapons.Current.Ammo;
                        }
                        LastShot = false;
                        RelShot();
                    }
                    LastShot = false;
                    RelShot();
                }
                else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch"))
                {
                    if (Player.Character.Weapons.Current.AmmoInClip > ClipAmmo)
                    {
                        Player.Character.Weapons.Current.AmmoInClip = ClipAmmo;
                        Player.Character.Weapons.Current.Ammo = TotalAmmo;
                        if (LastShot == true)
                        {
                            Player.Character.Weapons.Current.Ammo -= 1;
                        }
                    }
                    if ((Player.Character.Weapons.Current.AmmoInClip == 0 || LastShot == true) && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
                    {
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                        if (shotrel <= 0.4)
                        {
                            GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", 0.5);
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                        }
                        if (shotrel <= 0.85 && shotrel > 0.45)
                        {
                            return;
                        }
                        if (shotrel >= 0.85 && shotrel < 0.9)
                        {
                            Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                            Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                            if (LastShot == false)
                            {
                                Player.Character.Weapons.Current.Ammo -= 1;
                            }
                            ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                            TotalAmmo = Player.Character.Weapons.Current.Ammo;
                        }
                        LastShot = false;
                        RelShotC();
                    }
                    LastShot = false;
                    RelShotC();
                }
                else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload"))
                {
                    if (Player.Character.Weapons.Current.AmmoInClip > ClipAmmo)
                    {
                        Player.Character.Weapons.Current.AmmoInClip = ClipAmmo;
                        Player.Character.Weapons.Current.Ammo = TotalAmmo;
                        if (LastShot == true)
                        {
                            Player.Character.Weapons.Current.Ammo -= 1;
                        }
                    }
                    if ((Player.Character.Weapons.Current.AmmoInClip == 0 || LastShot == true) && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
                    {
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                        if (shotrel <= 0.4)
                        {
                            GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", 0.5);
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                        }
                        if (shotrel <= 0.85 && shotrel > 0.45)
                        {
                            return;
                        }
                        if (shotrel >= 0.85 && shotrel < 0.9)
                        {
                            Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                            Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                            if (LastShot == false)
                            {
                                Player.Character.Weapons.Current.Ammo -= 1;
                            }
                            ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                            TotalAmmo = Player.Character.Weapons.Current.Ammo;
                        }
                        LastShot = false;
                        RelShotB();
                    }
                    LastShot = false;
                    RelShotB();
                }
                else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch"))
                {
                    if (Player.Character.Weapons.Current.AmmoInClip > ClipAmmo)
                    {
                        Player.Character.Weapons.Current.AmmoInClip = ClipAmmo;
                        Player.Character.Weapons.Current.Ammo = TotalAmmo;
                        if (LastShot == true)
                        {
                            Player.Character.Weapons.Current.Ammo -= 1;
                        }
                    }
                    if ((Player.Character.Weapons.Current.AmmoInClip == 0 || LastShot == true) && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
                    {
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                        if (shotrel <= 0.4)
                        {
                            GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", 0.5);
                            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                        }
                        if (shotrel <= 0.85 && shotrel > 0.45)
                        {
                            return;
                        }
                        if (shotrel >= 0.85 && shotrel < 0.9)
                        {
                            Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                            Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                            if (LastShot == false)
                            {
                                Player.Character.Weapons.Current.Ammo -= 1;
                            }
                            ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                            TotalAmmo = Player.Character.Weapons.Current.Ammo;
                        }
                        LastShot = false;
                        RelShotBC();
                    }
                    LastShot = false;
                    RelShotBC();
                }
            }
        }

        private void ShotgunRel_KeyDown(object sender, GTA.KeyEventArgs e)
        {
            if (Game.isGameKeyPressed(GameKey.Jump) || Game.isGameKeyPressed(GameKey.EnterCar))
            {
                SpacePressed = true;
            }
        }

        private void RelShot()
        {
            if (SpacePressed == false && !Game.isGameKeyPressed(GameKey.Attack) && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))) && LastShot == false && Player.Character.Weapons.Current.AmmoInClip != 0 && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
            {
                if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                if (((shotrel >= 0.25 && shotrel < 0.9) || shotrel <= 0.10) && ReloadStart == false)
                {
                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", 0.15);
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                    ReloadStart = true;
                }
                if (shotrel <= 0.38 && shotrel >= 0.15)
                {
                    return;
                }
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload") && shotrel < 0.75 && shotrel >= 0.15 && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ReloadStart = false;
                    Player.Character.Weapons.Current.Ammo -= 1;
                    Player.Character.Weapons.Current.AmmoInClip += 1;
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                if (SpacePressed == false && !Game.isGameKeyPressed(GameKey.Attack) && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))))
                {
                    ReloadStart = false;
                    return;
                }
            }
            SpacePressed = false;
            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
            if (LastShot == false && ((shotrel <= 0.8 && shotrel > 0.1) || (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) <= 0))
            {
                GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", 0.9);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                Wait(50);
            }
        }

        private void RelShotC()
        {
            if (SpacePressed == false && !Game.isGameKeyPressed(GameKey.Attack) && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))) && LastShot == false && Player.Character.Weapons.Current.AmmoInClip != 0 && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
            {
                if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                if (((shotrel >= 0.25 && shotrel < 0.9) || shotrel <= 0.10) && ReloadStart == false)
                {
                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", 0.15);
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                    ReloadStart = true;
                }
                if (shotrel <= 0.38 && shotrel >= 0.15)
                {
                    return;
                }
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch") && shotrel < 0.75 && shotrel >= 0.15 && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ReloadStart = false;
                    Player.Character.Weapons.Current.Ammo -= 1;
                    Player.Character.Weapons.Current.AmmoInClip += 1;
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                if (SpacePressed == false && !Game.isGameKeyPressed(GameKey.Attack) && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))))
                {
                    ReloadStart = false;
                    return;
                }
            }
            SpacePressed = false;
            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
            if (LastShot == false && ((shotrel <= 0.8 && shotrel > 0.1) || (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) <= 0))
            {
                GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", 0.9);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                Wait(50);
            }
        }

        private void RelShotB()
        {
            if (SpacePressed == false && !Game.isGameKeyPressed(GameKey.Attack) && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))) && LastShot == false && Player.Character.Weapons.Current.AmmoInClip != 0 && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
            {
                if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                if (((shotrel >= 0.25 && shotrel < 0.9) || shotrel <= 0.10) && ReloadStart == false)
                {
                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", 0.15);
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                    ReloadStart = true;
                }
                if (shotrel <= 0.38 && shotrel >= 0.15)
                {
                    return;
                }
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload") && shotrel < 0.75 && shotrel >= 0.15 && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ReloadStart = false;
                    Player.Character.Weapons.Current.Ammo -= 1;
                    Player.Character.Weapons.Current.AmmoInClip += 1;
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                if (SpacePressed == false && !Game.isGameKeyPressed(GameKey.Attack) && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))))
                {
                    ReloadStart = false;
                    return;
                }
            }
            SpacePressed = false;
            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
            if (LastShot == false && ((shotrel <= 0.8 && shotrel > 0.1) || (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) <= 0))
            {
                GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", 0.9);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                Wait(50);
            }
        }

        private void RelShotBC()
        {
            if (SpacePressed == false && !Game.isGameKeyPressed(GameKey.Attack) && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))) && LastShot == false && Player.Character.Weapons.Current.AmmoInClip != 0 && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
            {
                if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                if (((shotrel >= 0.25 && shotrel < 0.9) || shotrel <= 0.10) && ReloadStart == false)
                {
                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", 0.15);
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                    ReloadStart = true;
                }
                if (shotrel <= 0.38 && shotrel >= 0.15)
                {
                    return;
                }
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch") && shotrel < 0.75 && shotrel >= 0.15 && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ReloadStart = false;
                    Player.Character.Weapons.Current.Ammo -= 1;
                    Player.Character.Weapons.Current.AmmoInClip += 1;
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                if (Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    ClipAmmo = Player.Character.Weapons.Current.AmmoInClip;
                    TotalAmmo = Player.Character.Weapons.Current.Ammo;
                }
                if (SpacePressed == false && !Game.isGameKeyPressed(GameKey.Attack) && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))))
                {
                    ReloadStart = false;
                    return;
                }
            }
            SpacePressed = false;
            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
            if (LastShot == false && ((shotrel <= 0.8 && shotrel > 0.1) || (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) <= 0))
            {
                GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", 0.9);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                Wait(50);
            }
        }
    }
}