using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;

namespace ShotgunRel
{

    public class ShotgunRel : Script
    {
        GTA.Native.Pointer shotrel = 0.0;
        List<int> clipList = new List<int>();
        bool SpacePressed = false;

        public ShotgunRel()
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
                if (Player.Character.Weapons.Current == Weapon.Shotgun_Basic)
                {
                    if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload"))
                    {
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                        if (Player.Character.Weapons.Current.AmmoInClip == 0 && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
                        {
                            if (!GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                            {
                                while (shotrel <= 0.4)
                                {
                                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", 0.5);
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                                }
                                while (shotrel <= 0.85 && shotrel > 0.45)
                                {
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                                    Wait(0);
                                }
                                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload") && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                    Player.Character.Weapons.Current.Ammo -= 1;
                                }
                                else if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip = 0;
                                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                                }
                                RelShot();
                                return;
                            }
                            else if (GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                            {
                                while (shotrel <= 0.4)
                                {
                                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", 0.5);
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                                }
                                while (shotrel <= 0.85 && shotrel > 0.45)
                                {
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                                    Wait(0);
                                }
                                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                }
                                else if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip -= Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.Ammo += Player.Character.Weapons.Current.MaxAmmoInClip;
                                }
                                RelShot();
                                return;
                            }
                        }
                        RelShot();
                        return;
                    }
                    else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch"))
                    {
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                        if (Player.Character.Weapons.Current.AmmoInClip == 0 && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
                        {
                            if (!GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                            {
                                while (shotrel <= 0.4)
                                {
                                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", 0.5);
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                                }
                                while (shotrel <= 0.85 && shotrel > 0.45)
                                {
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                                    Wait(0);
                                }
                                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                    Player.Character.Weapons.Current.Ammo -= 1;
                                }
                                else if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip = 0;
                                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                                }
                                RelShotC();
                                return;
                            }
                            else if (GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                            {
                                while (shotrel <= 0.4)
                                {
                                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", 0.5);
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                                }
                                while (shotrel <= 0.85 && shotrel > 0.45)
                                {
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                                    Wait(0);
                                }
                                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                }
                                else if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip -= Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.Ammo += Player.Character.Weapons.Current.MaxAmmoInClip;
                                }
                                RelShotC();
                                return;
                            }
                        }
                        RelShotC();
                        return;
                    }
                }
                else if (Player.Character.Weapons.Current == Weapon.Shotgun_Baretta)
                {
                    if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload"))
                    {
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                        if (Player.Character.Weapons.Current.AmmoInClip == 0 && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
                        {
                            if (!GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                            {
                                while (shotrel <= 0.4)
                                {
                                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", 0.5);
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                                }
                                while (shotrel <= 0.85 && shotrel > 0.45)
                                {
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                                    Wait(0);
                                }
                                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload") && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                    Player.Character.Weapons.Current.Ammo -= 1;
                                }
                                else if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip = 0;
                                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                                }
                                RelShotB();
                                return;
                            }
                            else if (GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                            {
                                while (shotrel <= 0.4)
                                {
                                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", 0.5);
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                                }
                                while (shotrel <= 0.85 && shotrel > 0.45)
                                {
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                                    Wait(0);
                                }
                                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                }
                                else if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip -= Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.Ammo += Player.Character.Weapons.Current.MaxAmmoInClip;
                                }
                                RelShotB();
                                return;
                            }
                        }
                        RelShotB();
                        return;
                    }
                    else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch"))
                    {
                        GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                        if (Player.Character.Weapons.Current.AmmoInClip == 0 && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0)
                        {
                            if (!GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                            {
                                while (shotrel <= 0.4)
                                {
                                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", 0.5);
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                                }
                                while (shotrel <= 0.85 && shotrel > 0.45)
                                {
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                                    Wait(0);
                                }
                                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                    Player.Character.Weapons.Current.Ammo -= 1;
                                }
                                else if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip = 0;
                                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                                }
                                RelShotBC();
                                return;
                            }
                            else if (GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character))
                            {
                                while (shotrel <= 0.4)
                                {
                                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", 0.5);
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                                }
                                while (shotrel <= 0.85 && shotrel > 0.45)
                                {
                                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                                    Wait(0);
                                }
                                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip += Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.AmmoInClip -= (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - 1);
                                }
                                else if (!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                                {
                                    Player.Character.Weapons.Current.AmmoInClip -= Player.Character.Weapons.Current.MaxAmmoInClip;
                                    Player.Character.Weapons.Current.Ammo += Player.Character.Weapons.Current.MaxAmmoInClip;
                                }
                                RelShotBC();
                                return;
                            }
                        }
                        RelShotBC();
                        return;
                    }
                }
            }
            return;
        }

        private void ShotgunRel_KeyDown(object sender, GTA.KeyEventArgs e)
        {
            if (e.Key == Keys.Space)
            {
                SpacePressed = true;
            }
        }

        private void RelShot()
        {
            while (Player.Character.Weapons.Current.AmmoInClip != 0 && SpacePressed == false && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0 && !Game.isGameKeyPressed(GameKey.Attack) && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))))
            {
                clipList.Add(Player.Character.Weapons.Current.AmmoInClip);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                while (shotrel >= 0.25 || shotrel <= 0.10)
                {
                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", 0.15);
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                }
                while (shotrel <= 0.38 && shotrel > 0.15)
                {
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                    Wait(0);
                }
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload") && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.Ammo -= 1;
                    Player.Character.Weapons.Current.AmmoInClip += 1;
                }
                else if ((!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload")) && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.AmmoInClip = (clipList[0]);
                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                }
                else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.AmmoInClip = (clipList[0] + 1);
                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                }
                clipList.Clear();
                return;
            }
            SpacePressed = false;
            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
            while ((shotrel <= 0.8 && shotrel > 0.1) || (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) <= 0)
            {
                GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", 1.0);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload", shotrel);
                Wait(0);
            }
            Wait(0);
            return;
        }

        private void RelShotC()
        {
            while (Player.Character.Weapons.Current.AmmoInClip != 0 && SpacePressed == false && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0 && !Game.isGameKeyPressed(GameKey.Attack) && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))))
            {
                clipList.Add(Player.Character.Weapons.Current.AmmoInClip);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                while (shotrel >= 0.25 || shotrel <= 0.10)
                {
                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", 0.15);
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                }
                while (shotrel <= 0.38 && shotrel > 0.15)
                {
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
                    Wait(0);
                }
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.Ammo -= 1;
                    Player.Character.Weapons.Current.AmmoInClip += 1;
                }
                else if ((!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch")) && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.AmmoInClip = (clipList[0]);
                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                }
                else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@shotgun", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.AmmoInClip = (clipList[0] + 1);
                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                }
                clipList.Clear();
                return;
            }
            SpacePressed = false;
            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
            while ((shotrel <= 0.8 && shotrel > 0.1) || (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) <= 0)
            {
                GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", 1.0);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@shotgun", "reload_crouch", shotrel);
            }
            Wait(0);
            return;
        }

        private void RelShotB()
        {
            while (Player.Character.Weapons.Current.AmmoInClip != 0 && SpacePressed == false && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0 && !Game.isGameKeyPressed(GameKey.Attack) && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))))
            {
                clipList.Add(Player.Character.Weapons.Current.AmmoInClip);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                while (shotrel >= 0.25 || shotrel <= 0.10)
                {
                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", 0.15);
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                }
                while (shotrel <= 0.38 && shotrel > 0.15)
                {
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                    Wait(0);
                }
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload") && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.Ammo -= 1;
                    Player.Character.Weapons.Current.AmmoInClip += 1;
                }
                else if ((!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload")) && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.AmmoInClip = (clipList[0]);
                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                }
                else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.AmmoInClip = (clipList[0] + 1);
                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                }
                clipList.Clear();
                return;
            }
            SpacePressed = false;
            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
            while ((shotrel <= 0.8 && shotrel > 0.1) || (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) <= 0)
            {
                GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", 1.0);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload", shotrel);
                Wait(0);
            }
            Wait(0);
            return;
        }

        private void RelShotBC()
        {
            while (Player.Character.Weapons.Current.AmmoInClip != 0 && SpacePressed == false && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip && Player.Character.isAlive && !Player.Character.isGettingUp && !Player.Character.isRagdoll && (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) > 0 && !Game.isGameKeyPressed(GameKey.Attack) && !Game.isGameKeyPressed(GameKey.Jump) && !(GTA.Native.Function.Call<bool>("IS_PED_IN_COVER", Player.Character) && (Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveLeft))))
            {
                clipList.Add(Player.Character.Weapons.Current.AmmoInClip);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                while (shotrel >= 0.25 || shotrel <= 0.10)
                {
                    GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", 0.15);
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                }
                while (shotrel <= 0.38 && shotrel > 0.15)
                {
                    GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
                    Wait(0);
                }
                if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip != Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.Ammo -= 1;
                    Player.Character.Weapons.Current.AmmoInClip += 1;
                }
                else if ((!GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch")) && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.AmmoInClip = (clipList[0]);
                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                }
                else if (GTA.Native.Function.Call<bool>("IS_CHAR_PLAYING_ANIM", Player.Character, "gun@baretta", "reload_crouch") && Player.Character.Weapons.Current.AmmoInClip == Player.Character.Weapons.Current.MaxAmmoInClip)
                {
                    Player.Character.Weapons.Current.AmmoInClip = (clipList[0] + 1);
                    Player.Character.Weapons.Current.Ammo += (Player.Character.Weapons.Current.MaxAmmoInClip - Player.Character.Weapons.Current.AmmoInClip);
                }
                clipList.Clear();
                return;
            }
            SpacePressed = false;
            GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
            while ((shotrel <= 0.8 && shotrel > 0.1) || (Player.Character.Weapons.Current.Ammo - Player.Character.Weapons.Current.AmmoInClip) <= 0)
            {
                GTA.Native.Function.Call("SET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", 1.0);
                GTA.Native.Function.Call("GET_CHAR_ANIM_CURRENT_TIME", Player.Character, "gun@baretta", "reload_crouch", shotrel);
            }
            Wait(0);
            return;
        }
    }
}
