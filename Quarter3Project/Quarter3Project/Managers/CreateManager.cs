#region File Description
// CreateManager.cs
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Quarter3Project.Managers
{
    public class CreateManager : DrawableGameComponent
    {

        #region Fields

        Game1 g;

        SpriteBatch spriteBatch;
        SpriteFont Consolas,
                   ConsolasLarge,
                   ConsolasSmall,
                   ConsolasTiny;

        MouseState mouseState,
                   prevMouseState;
        Vector2 mousePos;

        public List<ItemData.chr> chrSelection;
        Texture2D h,
                  k,
                  w;
        float supermansafetytimer = 0.0f;
        bool supermansafetylock = false;

        List<ItemData.skill> skillList;
        Texture2D wizPassive,
                  wizOffense, 
                  cleActive, 
                  cleOffense, 
                  warOffensive, 
                  warActive;
        List<ItemData.skillDescr> skillDList;

        List<ItemData.btn> btnList2;
        Texture2D btn, 
                  btn2, 
                  arrows;

        ButtonEvents bE;

        Texture2D bg, 
                  bbg, 
                  border1, 
                  gameBG;

        #endregion  
      
        #region Initialization

        public CreateManager(Game1 G)
            : base(G)
        {
            g = G;
        }

        public override void Initialize()
        {

            chrSelection = new List<ItemData.chr>();

            btnList2 = new List<ItemData.btn>();

            bE = new ButtonEvents(g);

            skillList = new List<ItemData.skill>();

            skillDList = new List<ItemData.skillDescr>();

            base.Initialize();

            btnList2.Add(new ItemData.btn(arrows, new Vector2(0, 0), new Vector2(40, (GraphicsDevice.Viewport.Height / 2) - (200 / 2)), new Point(30, 200), "1", Color.White, false, false, true, 500, 500));
            btnList2.Add(new ItemData.btn(arrows, new Vector2(30, 0), new Vector2(880, (GraphicsDevice.Viewport.Height / 2) - (200 / 2)), new Point(30, 200), "2", Color.White, false, false, true, 500, 501));
            btnList2.Add(new ItemData.btn(btn, new Vector2(0, 0), new Point(50, 35), "Back", Color.White, false, false, true, 500, 502));
            btnList2.Add(new ItemData.btn(btn2, new Vector2(375, 420), new Point(200, 80), "Start", Color.White, false, false, true, 500, 503));

            //skillList.Add(new ItemData.skill(btn2, new Vector2(150, 450), new Point(50, 50), 0, 0)); // Attack - Offensive - Swing your weapon to hit enemies
            skillList.Add(new ItemData.skill(wizOffense, new Vector2(150, 450), new Point(50, 50), 100, 10000)); // Fire Bolt - Offensive - Shoots fire projectile at target.
            skillList.Add(new ItemData.skill(wizPassive, new Vector2(210, 450), new Point(50, 50), 100, 10100)); // Health Transmogrify - Passive - A percentage of damage taken will reduce mana instead of health.
            skillList.Add(new ItemData.skill(cleOffense, new Vector2(150, 450), new Point(50, 50), 200, 20000)); // Holy Arrow - Offensive - Shoots light arrow projectile at target
            skillList.Add(new ItemData.skill(cleActive, new Vector2(210, 450), new Point(50, 50), 200, 20200)); // Rejuvenation - Active - Uses mana to regain some health.
            skillList.Add(new ItemData.skill(warOffensive, new Vector2(150, 450), new Point(50, 50), 300, 30000)); // Power Strike - Offensive - Uses Mana to increase the damage of the strike.
            skillList.Add(new ItemData.skill(warActive, new Vector2(210, 450), new Point(50, 50), 300, 30200)); // Oblivion - Active - You become oblivious and ignore a percentage of damage.
            //skillList.Add(new ItemData.skill(arcOffense, new Vector2(150, 450), new Point(50, 50), 1, 40000)); // Triple Shot - Offensive - Fire three arrows at your target.
            //skillList.Add(new ItemData.skill(arcPassive, new Vector2(210, 450), new Point(50, 50), 1, 40100)); // Steady Aim - Active - Increases precision and critical damage.
            
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 100, 10000, "Fire Bolt", "Shoot a projectile \nof fire at your \ntarget.", "(Offensive)"));
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 100, 10100, "Health Transmogrify", "A percentage of damage \ntaken will reduce Mana \ninstead of Health.", "(Passive)"));
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 200, 20000, "Holy Arrow", "Shoots an arrow of \nlight at your \ntarget.", "(Offensive)"));
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 200, 20200, "Rejuvenation", "Uses Mana to regain \nsome Health.", "(Active)"));
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 300, 30000, "Power Strike", "Uses Mana to \nincrease the power \nof your attack.", "(Offensive)"));
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 300, 30200, "Oblivion", "You become oblivious \nand ignore a \npercentage of \ndamage.", "(Offensive)"));

            chrSelection.Add(new ItemData.chr(w, new Vector2(0, 0), "Wizard", "The Wizard is mana based and \nis more powerful than the \nCleric. This profession is able \nto do a lot of damage and can \nalso take a lot of damage by \nbeing able to reduce a \npercentage of mana instead \nof health based on the \namount of damage taken.", 100, false));
            chrSelection.Add(new ItemData.chr(h, new Vector2(0, 0), "Cleric", "The Cleric is a mana based \nprofession, all of its skills \nrequire mana to be used. The \nCleric gets its power from the \nFaith attribute, this means \nthat Clerics are stronger \nagainst undead and unholy \nbeasts. Although this \nprofession has a low \namount of health it is made \nup by being able to regain \nhealth at a cost to \nsome mana.", 200, true));
            chrSelection.Add(new ItemData.chr(k, new Vector2(0, 0), "Knight", "The Knight is based on health, \nbeing able to take a lot of \ndamage and do a lot of damage. \nAs a Knight you are able to do \npowerful attacks at a cost to \nmana, you can also ignore \ndamage while under the \neffects of Oblivion.", 300, false));
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");
            ConsolasLarge = Game.Content.Load<SpriteFont>(@"Fonts/ConsolasLarge");
            ConsolasSmall = Game.Content.Load<SpriteFont>(@"Fonts/consolassmall");
            ConsolasTiny = Game.Content.Load<SpriteFont>(@"Fonts/ConsolasTiny");

            bg = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            bg.SetData<Color>(new Color[] { new Color(255, 255, 255, 0.7f) });
            bbg = new Texture2D(GraphicsDevice, 1, 1, false , SurfaceFormat.Color);
            bbg.SetData<Color>(new Color[] { new Color(0, 0, 0, .7f) });
            btn = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            btn.SetData<Color>(new Color[] { new Color(0.0f, 0.0f, 0.0f, 0.7f) });
            btn2 = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            btn2.SetData<Color>(new Color[] { new Color(0.0f, 0.0f, 0.0f, 0.7f) });
            border1 = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            border1.SetData<Color>(new Color[] { new Color(255, 255, 255) });

            gameBG = Game.Content.Load<Texture2D>(@"Images/gameBG");
            arrows = Game.Content.Load<Texture2D>(@"Images/ccArrows");            

            h = Game.Content.Load<Texture2D>(@"Images/Healer");
            k = Game.Content.Load<Texture2D>(@"Images/Knight");
            w = Game.Content.Load<Texture2D>(@"Images/Wizard");
            
            wizPassive = Game.Content.Load<Texture2D>(@"Images/passive");
            wizOffense = Game.Content.Load<Texture2D>(@"Images/Spell_Icon");
            cleOffense = Game.Content.Load<Texture2D>(@"Images/cleric_offensive");
            cleActive = Game.Content.Load<Texture2D>(@"Images/cleric_passive");
            warOffensive = Game.Content.Load<Texture2D>(@"Images/warr_offensive");
            warActive = Game.Content.Load<Texture2D>(@"Images/warr_defensive");

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);

            for (int i = 0; i < skillDList.Count; i++)
            {
                if (((int)skillDList[i].position.Y + skillDList[i].size.Y) >= GraphicsDevice.Viewport.Height && (int)mousePos.Y + skillDList[i].size.Y > GraphicsDevice.Viewport.Height)
                {
                    skillDList[i] = new ItemData.skillDescr(skillDList[i].background, new Vector2(mousePos.X, (int)skillDList[i].position.Y), skillDList[i].size, skillDList[i].popid, skillDList[i].skillid, skillDList[i].skillName, skillDList[i].skillDesc, skillDList[i].skillType);
                }
                else
                {
                    skillDList[i] = new ItemData.skillDescr(skillDList[i].background, new Vector2(mousePos.X, mousePos.Y), skillDList[i].size, skillDList[i].popid, skillDList[i].skillid, skillDList[i].skillName, skillDList[i].skillDesc, skillDList[i].skillType);
                }
            }

            foreach (ItemData.btn b in btnList2)
            {
                if (b.collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        switch (b.text.ToUpper())
                        {
                            case "BACK":
                                bE.setMenu();
                                break;
                            case "START":
                                bE.saveNewChr2();
                                break;
                        }
                    }
                }
            }

            if (supermansafetylock == true)
            {
                if (supermansafetytimer > 0)
                {
                    supermansafetytimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (supermansafetytimer <= 0)
                {
                    supermansafetylock = false;
                }
            }

            for (int i = 0; i < btnList2.Count; i++)
            {
                if (btnList2[i].collisionRect2().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        switch (btnList2[i].text.ToString())
                        {
                            case "1":
                                for (int s = 0; s < chrSelection.Count; s++)
                                {
                                    if (chrSelection[s].visible == true)
                                    {
                                        if (chrSelection[s].id == 100 && supermansafetylock == false)
                                        {
                                            chrSelection[0] = new ItemData.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, false);
                                            chrSelection[1] = new ItemData.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, true);
                                            supermansafetylock = true;
                                            supermansafetytimer = 0.4f;
                                        }
                                        else if (chrSelection[s].id == 200 && supermansafetylock == false)
                                        {
                                            chrSelection[1] = new ItemData.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, false);
                                            chrSelection[2] = new ItemData.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, true);
                                            supermansafetylock = true;
                                            supermansafetytimer = 0.4f;
                                        }
                                        else if (chrSelection[s].id == 300 && supermansafetylock == false)
                                        {
                                            chrSelection[2] = new ItemData.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, false);
                                            chrSelection[0] = new ItemData.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, true);
                                            supermansafetylock = true;
                                            supermansafetytimer = 0.4f;
                                        }
                                    }
                                }
                                    break;
                            case "2":
                                    for (int s = 0; s < chrSelection.Count; s++)
                                    {
                                        if (chrSelection[s].visible == true)
                                        {
                                            if (chrSelection[s].id == 100 && supermansafetylock == false)
                                            {
                                                chrSelection[2] = new ItemData.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, true);
                                                chrSelection[0] = new ItemData.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, false);
                                                supermansafetylock = true;
                                                supermansafetytimer = 0.4f;
                                            }
                                            else if (chrSelection[s].id == 200 && supermansafetylock == false)
                                            {
                                                chrSelection[0] = new ItemData.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, true);
                                                chrSelection[1] = new ItemData.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, false);
                                                supermansafetylock = true;
                                                supermansafetytimer = 0.4f;
                                            }
                                            else if (chrSelection[s].id == 300 && supermansafetylock == false)
                                            {
                                                chrSelection[1] = new ItemData.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, true);
                                                chrSelection[2] = new ItemData.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, false);
                                                supermansafetylock = true;
                                                supermansafetytimer = 0.4f;
                                            }
                                        }
                                    }
                                break;
                        }
                    }
                }
            }

                base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            spriteBatch.Draw(gameBG, new Rectangle(0, 0, 960, 620), Color.White);

            spriteBatch.Draw(bg, new Rectangle((GraphicsDevice.Viewport.Width / 2) - 400, (GraphicsDevice.Viewport.Height / 2) - 250, 800, 500), Color.White);

            foreach (ItemData.btn b in btnList2)
            {
                if (b.text == "" || b.text == "1" || b.text == "2")
                {
                    spriteBatch.Draw(bg, new Rectangle((int)b.position2.X, (int)b.position2.Y, 40, 200), Color.White);
                    spriteBatch.Draw(b.btnTexture, new Vector2(b.position2.X + 5, b.position2.Y), new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);
                }              
            }
            
            foreach (ItemData.chr c in chrSelection)
            {
                if (c.visible == true)
                {
                    spriteBatch.Draw(c.tex, new Rectangle(((GraphicsDevice.Viewport.Width / 2) + (int)c.pos.X) - (c.tex.Bounds.Width / 2), ((GraphicsDevice.Viewport.Height / 2) + (int)c.pos.Y) - (c.tex.Bounds.Height / 2), c.tex.Bounds.Width, c.tex.Bounds.Height), Color.White);
                }
            }

            spriteBatch.End();
            spriteBatch.Begin(); //Fonts act strange w/ NonPremultiplied blendstate, moved down here

            foreach (ItemData.btn b in btnList2)
            {
                if (b.text == "" || b.text == "1" || b.text == "2")
                {
                    
                }
                else
                {
                    spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);
                    spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 2) + 1, ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2) + 1), Color.Black);
                    spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 2), ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2)), Color.White);
                }
            }


            foreach (ItemData.chr c in chrSelection)
            {
                if (c.visible == true)
                {
                    spriteBatch.DrawString(ConsolasLarge, c.name, new Vector2((GraphicsDevice.Viewport.Width / 2) - (Consolas.MeasureString(c.name).Length() / 2 + 10), (GraphicsDevice.Viewport.Height / 2) - 240), Color.Black);
                    spriteBatch.DrawString(Consolas, c.descr, new Vector2(600, (GraphicsDevice.Viewport.Height / 2) - 200), Color.Black);
                    spriteBatch.DrawString(Consolas, "Skills", new Vector2(130, 420), Color.Black);

                    switch (c.id)
                    {
                        case 100:
                            spriteBatch.DrawString(Consolas, "Attributes: \n\n Power: 5 \n\n Presage: 15 \n\n Agility: 5 \n\n Faith: 5", new Vector2(130, (GraphicsDevice.Viewport.Height / 2) - 200), Color.Black);
                            for (int i = 0; i < skillList.Count; i++)
                            {
                                if (skillList[i].classid == 100)
                                {
                                    spriteBatch.Draw(skillList[i].texture, new Rectangle((int)skillList[i].position.X, (int)skillList[i].position.Y, skillList[i].size.X, skillList[i].size.Y), Color.White);

                                }
                            }

                            for (int i = 0; i < skillList.Count; i++)
                            {
                                if (skillList[i].collisionRect().Contains(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                                {
                                    for (int z = 0; z < skillDList.Count; z++)
                                    {
                                        if (skillList[i].skillid == skillDList[z].skillid && skillList[i].classid == 100)
                                        {
                                            spriteBatch.Draw(skillDList[z].background, new Rectangle((int)skillDList[z].position.X, (int)skillDList[z].position.Y, skillDList[z].size.X, skillDList[z].size.Y), Color.White);
                                            if (Consolas.MeasureString(skillDList[z].skillName).Length() > skillDList[z].size.X)
                                            {
                                                skillDList[z] = new ItemData.skillDescr(skillDList[z].background, skillDList[z].position, new Point((int)Consolas.MeasureString(skillDList[z].skillName).Length() + 10, skillDList[z].size.Y), skillDList[z].popid, skillDList[z].skillid, skillDList[z].skillName, skillDList[z].skillDesc, skillDList[z].skillType);
                                            }
                                            spriteBatch.DrawString(Consolas, skillDList[z].skillName, new Vector2(skillDList[z].position.X + 5, skillDList[z].position.Y + 5), Color.White);
                                            spriteBatch.DrawString(ConsolasTiny, skillDList[z].skillType, new Vector2(skillDList[z].position.X + 6, skillDList[z].position.Y + 1.5f + Consolas.MeasureString(skillDList[z].skillName).Y), Color.White);
                                            spriteBatch.DrawString(ConsolasSmall, skillDList[z].skillDesc, new Vector2(skillDList[z].position.X + 7.5f, skillDList[z].position.Y + 12.0f + Consolas.MeasureString(skillDList[z].skillName).Y), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + (int)2.5f, 1, skillDList[z].size.Y - 5), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle(((int)skillDList[z].position.X + skillDList[z].size.X) - (int)3.0f, (int)skillDList[z].position.Y + (int)2.5f, 1, skillDList[z].size.Y - 5), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + (int)2.5f, skillDList[z].size.X - 5, 1), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + skillDList[z].size.Y - (int)3.0f, skillDList[z].size.X - 5, 1), Color.White);

                                        }
                                    }
                                }
                            }
                            break;
                        case 200:
                            for (int i = 0; i < skillList.Count; i++)
                            {
                                if (skillList[i].classid == 200)
                                {
                                    spriteBatch.Draw(skillList[i].texture, new Rectangle((int)skillList[i].position.X, (int)skillList[i].position.Y, skillList[i].size.X, skillList[i].size.Y), Color.White);
                                    spriteBatch.DrawString(Consolas, "Attributes: \n\n Power: 5 \n\n Presage: 10 \n\n Agility: 5 \n\n Faith: 15", new Vector2(130, (GraphicsDevice.Viewport.Height / 2) - 200), Color.Black);
                                }
                            }

                            for (int i = 0; i < skillList.Count; i++)
                            {
                                if (skillList[i].collisionRect().Contains(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                                {
                                    for (int z = 0; z < skillDList.Count; z++)
                                    {
                                        if (skillList[i].skillid == skillDList[z].skillid && skillList[i].classid == 200)
                                        {
                                            spriteBatch.Draw(skillDList[z].background, new Rectangle((int)skillDList[z].position.X, (int)skillDList[z].position.Y, skillDList[z].size.X, skillDList[z].size.Y), Color.White);
                                            if (Consolas.MeasureString(skillDList[z].skillName).Length() > skillDList[z].size.X)
                                            {
                                                skillDList[z] = new ItemData.skillDescr(skillDList[z].background, skillDList[z].position, new Point((int)Consolas.MeasureString(skillDList[z].skillName).Length() + 10, skillDList[z].size.Y), skillDList[z].popid, skillDList[z].skillid, skillDList[z].skillName, skillDList[z].skillDesc, skillDList[z].skillType);
                                            }
                                            spriteBatch.DrawString(Consolas, skillDList[z].skillName, new Vector2(skillDList[z].position.X + 5, skillDList[z].position.Y + 5), Color.White);
                                            spriteBatch.DrawString(ConsolasTiny, skillDList[z].skillType, new Vector2(skillDList[z].position.X + 6, skillDList[z].position.Y + 1.5f + Consolas.MeasureString(skillDList[z].skillName).Y), Color.White);
                                            spriteBatch.DrawString(ConsolasSmall, skillDList[z].skillDesc, new Vector2(skillDList[z].position.X + 7.5f, skillDList[z].position.Y + 12.0f + Consolas.MeasureString(skillDList[z].skillName).Y), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + (int)2.5f, 1, skillDList[z].size.Y - 5), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle(((int)skillDList[z].position.X  + skillDList[z].size.X) - (int)3.0f, (int)skillDList[z].position.Y + (int)2.5f, 1, skillDList[z].size.Y - 5), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + (int)2.5f, skillDList[z].size.X - 5, 1), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + skillDList[z].size.Y - (int)3.0f, skillDList[z].size.X - 5, 1), Color.White);
                                        }
                                    }
                                }
                            }
                            break;
                        case 300:
                            for (int i = 0; i < skillList.Count; i++)
                            {
                                if (skillList[i].classid == 300)
                                {
                                    spriteBatch.Draw(skillList[i].texture, new Rectangle((int)skillList[i].position.X, (int)skillList[i].position.Y, skillList[i].size.X, skillList[i].size.Y), Color.White);
                                    spriteBatch.DrawString(Consolas, "Attributes: \n\n Power: 15 \n\n Presage: 5 \n\n Agility: 5 \n\n Faith: 5", new Vector2(130, (GraphicsDevice.Viewport.Height / 2) - 200), Color.Black);
                                }
                            }

                            for (int i = 0; i < skillList.Count; i++)
                            {
                                if (skillList[i].collisionRect().Contains(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                                {
                                    for (int z = 0; z < skillDList.Count; z++)
                                    {
                                        if (skillList[i].skillid == skillDList[z].skillid && skillList[i].classid == 300)
                                        {
                                            spriteBatch.Draw(skillDList[z].background, new Rectangle((int)skillDList[z].position.X, (int)skillDList[z].position.Y, skillDList[z].size.X, skillDList[z].size.Y), Color.White);
                                            if (Consolas.MeasureString(skillDList[z].skillName).Length() > skillDList[z].size.X)
                                            {
                                                skillDList[z] = new ItemData.skillDescr(skillDList[z].background, skillDList[z].position, new Point((int)Consolas.MeasureString(skillDList[z].skillName).Length() + 10, skillDList[z].size.Y), skillDList[z].popid, skillDList[z].skillid, skillDList[z].skillName, skillDList[z].skillDesc, skillDList[z].skillType);
                                            }
                                            spriteBatch.DrawString(Consolas, skillDList[z].skillName, new Vector2(skillDList[z].position.X + 5, skillDList[z].position.Y + 5), Color.White);
                                            spriteBatch.DrawString(ConsolasTiny, skillDList[z].skillType, new Vector2(skillDList[z].position.X + 6, skillDList[z].position.Y + 1.5f + Consolas.MeasureString(skillDList[z].skillName).Y), Color.White);
                                            spriteBatch.DrawString(ConsolasSmall, skillDList[z].skillDesc, new Vector2(skillDList[z].position.X + 7.5f, skillDList[z].position.Y + 12.0f + Consolas.MeasureString(skillDList[z].skillName).Y), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + (int)2.5f, 1, skillDList[z].size.Y - 5), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle(((int)skillDList[z].position.X  + skillDList[z].size.X) - (int)3.0f, (int)skillDList[z].position.Y + (int)2.5f, 1, skillDList[z].size.Y - 5), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + (int)2.5f, skillDList[z].size.X - 5, 1), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[z].position.X + (int)2.5f, (int)skillDList[z].position.Y + skillDList[z].size.Y - (int)3.0f, skillDList[z].size.X - 5, 1), Color.White);
                                        }
                                    }
                                }
                            }
                            break;
                    }

                }

                
            }
            spriteBatch.End();
 	         base.Draw(gameTime);
        }

        #endregion

    }
}
