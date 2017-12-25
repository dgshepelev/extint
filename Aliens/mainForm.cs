using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliensGame
{
    public partial class mainForm : Form
    {
        /*timer and speed*/
        Timer timer = new Timer();
        int timerDelay = 10;// delay time for threads and timer (ms)
        int playerMovDistance = 5; //movement distance (px)
        int playerTimerDelay = 10;
        int playerLives = 3;
        int alienTimerDelay = 12;//delay time for alien threads (ms)
        int alienMovDistance = 1; //alien movement distance (px)
        /*shots*/
        int shotNum = 3;//max total at once
        int shotDelay = 50;//timer cycles
        int shotDamage = 33;
        int shotDist = 10;
        /*aliens*/
        int totalAliens = 20;//total num per level
        int numAliens = 6;//max total at once
        int tickSpawnDelay = 25;//cycles between spawns
        Level game;
        int bonusFadeDelay=250;
        int screen;
        bool paused = false;
        bool restartGame = false;

        private delegate void setTopCallback(int val);
        private delegate void setLeftCallback(int val);
        public mainForm()
        {
            InitializeComponent();
        }
        //open/close main menu
        private void OpenMain()
        {
            if (restartGame)
            {
                restartGame = false;
                paused = false;
            }
            if (paused&&!this.game.IsOver())
            {
                btRes.Enabled = true;
                btRes.Visible = true;
            }
            btStart.Enabled = true;
            btSets.Enabled = true;
            btQuit.Enabled = true;
            btStart.Visible = true;
            btSets.Visible = true;
            btQuit.Visible = true;
            btQuit.Enabled = true;
            logoPic.Visible = true;
            logoPic.Image = global::AliensGame.Properties.Resources.logo;
            logoPic.SetBounds((this.Width - 710) / 2, 100, 710, 202);
            screen = 0;
        }
        private void CloseMain()
        {
            btRes.Enabled = false;
            btStart.Enabled = false;
            btSets.Enabled = false;
            btQuit.Enabled = false;
            btRes.Visible = false;
            btStart.Visible = false;
            btSets.Visible = false;
            btQuit.Visible = false;
            logoPic.Visible = false;
        }
        //open/close settings menu
        private void CloseSets()
        {
            sLbl0.Visible = false;
            sLbl1.Visible = false;
            sLbl2.Visible = false;
            sLbl3.Visible = false;
            sLbl4.Visible = false;
            sLbl6.Visible = false;
            sLbl8.Visible = false;
            sLblA.Visible = false;
            sLblC.Visible = false;
            btMin0.Visible = false;
            btMin1.Visible = false;
            btMin2.Visible = false;
            btMin3.Visible = false;
            btMin4.Visible = false;
            btMin6.Visible = false;
            btMin8.Visible = false;
            edt0.Visible = false;
            edt1.Visible = false;
            edt2.Visible = false;
            edt3.Visible = false;
            edt4.Visible = false;
            edt6.Visible = false;
            edt8.Visible = false;
            btPlus0.Visible = false;
            btPlus1.Visible = false;
            btPlus2.Visible = false;
            btPlus3.Visible = false;
            btPlus4.Visible = false;
            btPlus6.Visible = false;
            btPlus8.Visible = false;
            btBack.Visible = false;
        }
        private void OpenSets()
        {
            sLbl0.Visible = true;
            sLbl1.Visible = true;
            sLbl2.Visible = true;
            sLbl3.Visible = true;
            sLbl4.Visible = true;
            sLbl6.Visible = true;
            sLbl8.Visible = true;
            sLblA.Visible = true;
            sLblC.Visible = true;
            btMin0.Visible = true;
            btMin1.Visible = true;
            btMin2.Visible = true;
            btMin3.Visible = true;
            btMin4.Visible = true;
            btMin6.Visible = true;
            btMin8.Visible = true;
            edt0.Visible = true;
            edt0.Text = "" + timerDelay;
            edt1.Visible = true;
            edt1.Text = "" + playerMovDistance;
            edt2.Visible = true;
            edt2.Text = "" + shotDist;
            edt3.Visible = true;
            edt3.Text = "" + playerLives;
            edt4.Visible = true;
            edt4.Text = "" + alienMovDistance;
            edt6.Visible = true;
            edt6.Text = "" + totalAliens;
            edt8.Visible = true;
            edt8.Text = "" + tickSpawnDelay;
            btPlus0.Visible = true;
            btPlus1.Visible = true;
            btPlus2.Visible = true;
            btPlus3.Visible = true;
            btPlus4.Visible = true;
            btPlus6.Visible = true;
            btPlus8.Visible = true;
            btBack.Visible = true;
            logoPic.Visible = true;
            logoPic.SetBounds(50, 50, 236, 67);
            screen = 1;
        }
        //resumes/starts a new game
        private void OpenGame(bool resume)
        {
            lblLevel.Visible = true;
            lblScore.Visible = true;
            lblHealth.Visible = true;
            lblLives.Visible = true;
            lblTime.Visible = true;
            plrPic.Visible = true;
            paused = false;
            if (resume) game.GameResume();
            else
            {
                if (this.game == null)//if game doesn't exist, creates a new one
                {
                    this.game = new Level(this, timerDelay, alienTimerDelay, alienMovDistance, shotDelay, shotNum, shotDamage, bonusFadeDelay, playerMovDistance, playerTimerDelay, playerLives, shotDist);
                }
                else//if games exists, resets it
                {
                    this.game.Kill();
                    this.game.Restart(timerDelay, alienTimerDelay, alienMovDistance, shotDelay, shotNum, shotDamage, bonusFadeDelay, playerMovDistance, playerTimerDelay, playerLives, shotDist);
                }
                game.SetCells(this.Width / (88 * 2));
                this.game.Start(numAliens, totalAliens, tickSpawnDelay);//starts the first level
            }
            screen = 2;
        }
        //hides the titlebar, pauses game if pause=true
        private void PauseGame(bool pause)
        {
            lblLevel.Visible = false;
            lblScore.Visible = false;
            lblHealth.Visible = false;
            lblLives.Visible = false;
            plrPic.Visible = false;
            lblTime.Visible = false;
            if (pause)
            {
                game.GameHalt();
                paused = true;
            }
        }
        //form load events
        private void OnFormLoad(object sender, EventArgs e)
        {
            /* sets up keypress events */
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            /* Hides all objects and opens the main menu */
            CloseMain();
            CloseSets();
            PauseGame(false);
            OpenMain();
            /* sets and starts up timer */
            timer.Interval = timerDelay;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += new EventHandler(timerCycle);
        }
        /* form close event */
        private void OnFormClose(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        public void timerCycle(object sender, EventArgs e)
        {
            if (screen != 2 || this.game.IsOver()) return;//breaks if not ingame, or game is over
            if (this.game.playerWon==0)//checks if game has been won
            {
                this.game.playerWon = -1;
                if (game.level == 10)//wins if passes 10th level
                {
                    this.logoPic.Visible = true;
                    this.logoPic.Image = global::AliensGame.Properties.Resources.win;
                    this.logoPic.SetBounds((this.Width - 391) / 2, (this.Height - 100) / 3, 391, 100);
                    this.game.IsOver(true);
                    game.Kill();
                }
                else game.Start(game.level+1,numAliens, totalAliens, tickSpawnDelay);//starts the next level
                return;
            }
            this.game.timerCycle();
        }
        /* keypress events */
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (screen != 2 || this.game.PlrGetState() == 0) return;//halts if not ingame
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) this.game.PlrSetMov(2);
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) this.game.PlrSetMov(1);
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space) this.game.PlrSetFire(true);
            e.Handled = e.SuppressKeyPress = true;//prevents beeping if there's a hidden control selected
        }
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (screen == 0 && paused)//main
                {
                    CloseMain();
                    OpenGame(true);
                    return;
                }
                if (screen == 1)//settings
                {
                    CloseSets();
                    OpenMain();
                }
                if (screen == 2)//game
                {
                    if (!paused)
                    {
                        PauseGame(true);
                        OpenMain();
                        return;
                    }
                }
            }
            if (screen != 2 || this.game.PlrGetState() == 0) return;//halts if not ingame
            if (e.KeyCode == Keys.Z) this.game.AlienBonusAdd(4);//health boost
            if (e.KeyCode == Keys.X) this.game.AlienBonusAdd(3);//fire up
            if (e.KeyCode == Keys.C) this.game.AlienBonusAdd(6);//weapon up
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) this.game.PlrUnSetMov(2);
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) this.game.PlrUnSetMov(1);
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space)
            {
                this.game.PlrSetFire(false);
                this.game.ClrShotLock();//resets delay counter- will fire on next keypress
            }
        }
        /* main menu buttons */
        private void btSets_Click(object sender, EventArgs e)
        {
            CloseMain();
            OpenSets();
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            CloseMain();
            OpenGame(false);
            this.game.ScoreUpd(0);
            //new game bodge
            PauseGame(true);
            OpenGame(true);
        }
        private void btQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btRes_Click(object sender, EventArgs e)
        {
            CloseMain();
            OpenGame(true);
        }
        /* settings menu buttons */
        private void btBack_Click(object sender, EventArgs e)
        {
            CloseSets();
            OpenMain();
        }
        private void btMin0_Click(object sender, EventArgs e)
        {
            if (timerDelay > 1)
            {
                timerDelay--;
                timer.Interval = timerDelay;
                playerTimerDelay = timerDelay;
                alienTimerDelay = timerDelay;
            }
            edt0.Text = "" + timerDelay;
            restartGame = true;
        }
        private void btPlus0_Click(object sender, EventArgs e)
        {
            timerDelay++;
            edt0.Text = "" + timerDelay;
            restartGame = true;
        }
        private void btMin1_Click(object sender, EventArgs e)
        {
            if (playerMovDistance>1) playerMovDistance--;
            edt1.Text = "" + playerMovDistance;
            restartGame = true;
        }
        private void btPlus1_Click(object sender, EventArgs e)
        {
            if (playerMovDistance<9) playerMovDistance++;
            edt1.Text = "" + playerMovDistance;
            restartGame = true;
        }
        private void btMin2_Click(object sender, EventArgs e)
        {
            if (shotDist > 1) shotDist--;
            edt2.Text = "" + shotDist;
            restartGame = true;
        }
        private void btPlus2_Click(object sender, EventArgs e)
        {
            if (shotDist < 20) shotDist++;
            edt2.Text = "" + shotDist;
            restartGame = true;
        }
        private void btMin3_Click(object sender, EventArgs e)
        {
            if (playerLives>0) playerLives--;
            edt3.Text = "" + playerLives;
            restartGame = true;
        }
        private void btPlus3_Click(object sender, EventArgs e)
        {
            playerLives++;
            edt3.Text = "" + playerLives;
            restartGame = true;
        }
        private void btMin4_Click(object sender, EventArgs e)
        {
            if (alienMovDistance>1) alienMovDistance--;
            edt4.Text = "" + alienMovDistance;
            restartGame = true;
        }
        private void btPlus4_Click(object sender, EventArgs e)
        {
            if (alienMovDistance < 11) alienMovDistance++;
            edt4.Text = "" + alienMovDistance;
            restartGame = true;
        }
        private void btMin6_Click(object sender, EventArgs e)
        {
            if (totalAliens-1>=numAliens) totalAliens--;
            edt6.Text = "" + totalAliens;
            restartGame = true;
        }
        private void btPlus6_Click(object sender, EventArgs e)
        {
            totalAliens++;
            edt6.Text = "" + totalAliens;
            restartGame = true;
        }
        private void btMin8_Click(object sender, EventArgs e)
        {
            if (tickSpawnDelay>0) tickSpawnDelay--;
            edt8.Text = "" + tickSpawnDelay;
            restartGame = true;
        }
        private void btPlus8_Click(object sender, EventArgs e)
        {
            tickSpawnDelay++;
            edt8.Text = "" + tickSpawnDelay;
            restartGame = true;
        }

        //end of funcs
    }
    //end of class
}
