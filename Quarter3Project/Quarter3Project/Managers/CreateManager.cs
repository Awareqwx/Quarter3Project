using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Wizard Skill IDs:
 * 3000 - Mana Passive
 * 3001 - Fire Bolt 
 */

namespace Quarter3Project.Managers
{
    public class CreateManager : DrawableGameComponent
    {
        Game1 g;
        SpriteBatch spriteBatch;
        Texture2D bg, bbg, border1, btn, btn2, arrows, h, k, w, gameBG, wizPassive, wizOffense, clePassive, cleOffense;
        public List<ItemData.chr> chrSelection;
        List<ItemData.btn> btnList2;
        List<ItemData.skill> skillList;
        List<ItemData.skillDescr> skillDList;
        SpriteFont Consolas, ConsolasLarge, ConsolasSmall, ConsolasTiny;
        MouseState mouseState, prevMouseState;
        Vector2 mousePos;
        ButtonEvents bE;
        float supermansafetytimer = 0.0f;
        bool supermansafetylock = false;

        public CreateManager(Game1 G)
            : base(G)
        {
            g = G;
            chrSelection = new List<ItemData.chr>();
            btnList2 = new List<ItemData.btn>();
            bE = new ButtonEvents(G);
            skillList = new List<ItemData.skill>();
            skillDList = new List<ItemData.skillDescr>();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            arrows = Game.Content.Load<Texture2D>(@"Images/ccArrows");

            btnList2.Add(new ItemData.btn(arrows, new Vector2(0, 0), new Vector2(40, (GraphicsDevice.Viewport.Height / 2) - (200 / 2)), new Point(30, 200), "1", Color.White, false, false, true, 500, 500));
            btnList2.Add(new ItemData.btn(arrows, new Vector2(30, 0), new Vector2(880, (GraphicsDevice.Viewport.Height / 2) - (200 / 2)), new Point(30, 200), "2", Color.White, false, false, true, 500, 501));
            btnList2.Add(new ItemData.btn(btn, new Vector2(0, 0), new Point(50, 35), "Back", Color.White, false, false, true, 500, 502));
            btnList2.Add(new ItemData.btn(btn2, new Vector2(375, 420), new Point(200, 80), "Start", Color.White, false, false, true, 500, 503));

            h = Game.Content.Load<Texture2D>(@"Images/Healer");
            k = Game.Content.Load<Texture2D>(@"Images/Knight");
            w = Game.Content.Load<Texture2D>(@"Images/Wizard");
            gameBG = Game.Content.Load<Texture2D>(@"Images/gameBG");
            wizPassive = Game.Content.Load<Texture2D>(@"Images/passive");
            wizOffense = Game.Content.Load<Texture2D>(@"Images/Spell_Icon");
            cleOffense = Game.Content.Load<Texture2D>(@"Images/cleric_offensive");
            clePassive = Game.Content.Load<Texture2D>(@"Images/cleric_passive");

            skillList.Add(new ItemData.skill(wizPassive, new Vector2(150, 450), new Point(50, 50), 3, 3000)); // Wizard Passive
            skillList.Add(new ItemData.skill(wizOffense, new Vector2(210, 450), new Point(50, 50), 3, 3001)); // Wizard Offensive
            skillList.Add(new ItemData.skill(clePassive, new Vector2(150, 450), new Point(50, 50), 1, 1000)); // Cleric Passive
            skillList.Add(new ItemData.skill(cleOffense, new Vector2(210, 450), new Point(50, 50), 1, 1001)); // Cleric Offensive
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 3000, 3000, "Mana Heal", "Eats Mana instead\nof Health.", "(Passive)"));
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 3001, 3001, "Fire Bolt", "Shoots a bolt of\nfire. \n\nCost: 5MP\nDmg: 5", "(Active)"));
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 1000, 1000, "Regeneration", "Slowly regain\nhealth over time.", "(Passive)"));
            skillDList.Add(new ItemData.skillDescr(bbg, new Vector2(mousePos.X, mousePos.Y), new Point(150, 150), 1001, 1001, "Holy Arrow", "Conjure an arrow\nof light. \n\nCost: 5MP\nDmg: 5", "(Active)"));
           
            chrSelection.Add(new ItemData.chr(h, new Vector2(0, 0), "Healer", "This is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.", 1, true));
            chrSelection.Add(new ItemData.chr(k, new Vector2(0, 0), "Knight", "This is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.", 2, false));
            chrSelection.Add(new ItemData.chr(w, new Vector2(0, 0), "Wizard", "This is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.", 3, false));

            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");
            ConsolasLarge = Game.Content.Load<SpriteFont>(@"Fonts/ConsolasLarge");
            ConsolasSmall = Game.Content.Load<SpriteFont>(@"Fonts/consolassmall");
            ConsolasTiny = Game.Content.Load<SpriteFont>(@"Fonts/ConsolasTiny");

            base.LoadContent();
        }

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
                                        if (chrSelection[s].id == 1 && supermansafetylock == false)
                                        {
                                            chrSelection[0] = new ItemData.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, false);
                                            chrSelection[1] = new ItemData.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, true);
                                            supermansafetylock = true;
                                            supermansafetytimer = 0.4f;
                                        }
                                        else if (chrSelection[s].id == 2 && supermansafetylock == false)
                                        {
                                            chrSelection[1] = new ItemData.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, false);
                                            chrSelection[2] = new ItemData.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, true);
                                            supermansafetylock = true;
                                            supermansafetytimer = 0.4f;
                                        }
                                        else if (chrSelection[s].id == 3 && supermansafetylock == false)
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
                                            if (chrSelection[s].id == 1 && supermansafetylock == false)
                                            {
                                                chrSelection[2] = new ItemData.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, true);
                                                chrSelection[0] = new ItemData.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, false);
                                                supermansafetylock = true;
                                                supermansafetytimer = 0.4f;
                                            }
                                            else if (chrSelection[s].id == 2 && supermansafetylock == false)
                                            {
                                                chrSelection[0] = new ItemData.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, true);
                                                chrSelection[1] = new ItemData.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, false);
                                                supermansafetylock = true;
                                                supermansafetytimer = 0.4f;
                                            }
                                            else if (chrSelection[s].id == 3 && supermansafetylock == false)
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
                    spriteBatch.DrawString(Consolas, c.descr, new Vector2(600, (GraphicsDevice.Viewport.Height / 2) - 200), Color.Transparent);
                    spriteBatch.DrawString(Consolas, "Skills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\n", new Vector2(220, (GraphicsDevice.Viewport.Height / 2) - 200), Color.Transparent);
                    spriteBatch.DrawString(Consolas, "Skills", new Vector2(130, 420), Color.Black);

                    if (c.id == 1)
                    {
                        for (int i = 0; i < skillList.Count; i++)
                        {
                            if (skillList[i].classid == 1)
                            {
                                spriteBatch.Draw(skillList[i].texture, new Rectangle((int)skillList[i].position.X, (int)skillList[i].position.Y, skillList[i].size.X, skillList[i].size.Y), Color.White);
                            }
                        }
                        for (int i = 0; i < skillList.Count; i++)
                        {
                            if (skillList[i].classid == 1)
                            {
                                if (skillList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                                {
                                    for (int g = 0; g < skillDList.Count; g++)
                                    {
                                        if (skillDList[g].popid == skillList[i].skillid)
                                        {
                                            spriteBatch.Draw(skillDList[g].background, new Rectangle((int)skillDList[g].position.X, (int)skillDList[g].position.Y, skillDList[g].size.X, skillDList[g].size.Y), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[g].position.X + 5, (int)skillDList[g].position.Y + 5, 140, 1), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[g].position.X + 5, (int)skillDList[g].position.Y + 145, 140, 1), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[g].position.X + 5, (int)skillDList[g].position.Y + 5, 1, 140), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[g].position.X + 145, (int)skillDList[g].position.Y + 5, 1, 140), Color.White);
                                            spriteBatch.DrawString(Consolas, skillDList[i].skillName, new Vector2((int)skillDList[g].position.X + 10, (int)skillDList[g].position.Y + 5), Color.White);
                                            spriteBatch.DrawString(ConsolasTiny, skillDList[i].skillType, new Vector2((int)skillDList[g].position.X + 10, (int)skillDList[g].position.Y + 25), Color.White);
                                            spriteBatch.DrawString(ConsolasSmall, skillDList[i].skillDesc, new Vector2((int)skillDList[g].position.X + 10, (int)skillDList[g].position.Y + 40), Color.White);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (c.id == 2)
                    {

                    }
                    else if (c.id == 3)
                    {
                        for (int i = 0; i < skillList.Count; i++)
                        {
                            if (skillList[i].classid == 3)
                            {
                                spriteBatch.Draw(skillList[i].texture, new Rectangle((int)skillList[i].position.X, (int)skillList[i].position.Y, skillList[i].size.X, skillList[i].size.Y), Color.White);
                            }
                        }
                        for (int i = 0; i < skillList.Count; i++)
                        {
                            if (skillList[i].classid == 3)
                            {
                                if (skillList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                                {
                                    for (int g = 0; g < skillDList.Count; g++)
                                    {
                                        if (skillDList[g].popid == skillList[i].skillid)
                                        {
                                            spriteBatch.Draw(skillDList[g].background, new Rectangle((int)skillDList[g].position.X, (int)skillDList[g].position.Y, skillDList[g].size.X, skillDList[g].size.Y), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[g].position.X + 5, (int)skillDList[g].position.Y + 5, 140, 1), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[g].position.X + 5, (int)skillDList[g].position.Y + 145, 140, 1), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[g].position.X + 5, (int)skillDList[g].position.Y + 5, 1, 140), Color.White);
                                            spriteBatch.Draw(border1, new Rectangle((int)skillDList[g].position.X + 145, (int)skillDList[g].position.Y + 5, 1, 140), Color.White);
                                            spriteBatch.DrawString(Consolas, skillDList[i].skillName, new Vector2((int)skillDList[g].position.X + 10, (int)skillDList[g].position.Y + 5), Color.White);
                                            spriteBatch.DrawString(ConsolasTiny, skillDList[i].skillType, new Vector2((int)skillDList[g].position.X + 10, (int)skillDList[g].position.Y + 25), Color.White);
                                            spriteBatch.DrawString(ConsolasSmall, skillDList[i].skillDesc, new Vector2((int)skillDList[g].position.X + 10, (int)skillDList[g].position.Y + 40), Color.White);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                
            }
            spriteBatch.End();
 	         base.Draw(gameTime);
        }
    }
}
