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
    class Level
    {
        int cellNum;
        mainForm form;
        /*timer and speed*/
        int timerDelay;// delay time for threads and timer (ms)
        int playerMovDistance; //movement distance (px)
        int playerTimerDelay;
        int playerLives;
        int alienTimerDelay;//delay time for alien threads (ms)
        //misc
        Player plr;
        int score;
        public int level=0;
        Random rand=new Random();
        bool launch = false;
        /*shots*/
        int shotNum;//max total at once
        int shotDelay;//timer cycles
        int shotLock;//>0 == delay in cycles,0==unlocked
        int shotDamage;
        int shotDist;
        /*aliens*/
        int totalAliens;//total num per level
        int numAliens;//max total at once
        int oldNumAliens = 0;
        int numAliveAliens;//currently active
        int tickSpawnDelay;//cycles between spawns
        int spawnDelay;//cycles*aliens
        int spawnDelayCount;
        double posMultiplier; //for increasing alien's health each level
        double negMultiplier;
        Shot[] playerShot, alienShot;
        Alien[] alien;
        Bonus[] bonus;
        /*alien shots*/
        int oldAlienShotNum=0;
        int alienShotNum;//max total at once
        int alienShotDelay;//timer cycles
        int alienShotLock;//>0 == delay in cycles,0==unlocked
        int alienShotDamage;
        public int playerWon = -1;
        private bool gameOver = false;
        int bonusFadeDelay;
        int bonusCount = 0;
        int bonusNum = 0;
        int oldBonusNum = 0;
        int timerCount = 0;
        Timer timer = new Timer();
        private bool paused = false;

        private delegate void setTopCallback(int val);
        private delegate void setLeftCallback(int val);

        public bool IsOver()
        {
            return gameOver;
        }
        public void IsOver(bool v)
        {
            gameOver=v;
        }
        //player functions
        public void PlrSetFire(bool f)
        {
            this.plr.SetFire(f);
        }
        public int PlrGetState()
        {
            return this.plr.GetState();
        }
        public void PlrSetMov(int direction)
        {
            this.plr.SetMov(direction);
        }
        public void PlrUnSetMov(int direction)
        {
            if (direction == 1) this.plr.SetMov((this.plr.GetMov() == 1) ? 0 : 2);
            if (direction == 2) this.plr.SetMov((this.plr.GetMov() == 2) ? 0 : 1);
        }
        //resets shot lock
        public void ClrShotLock()
        {
            this.shotLock = 0;
        }
        //returns HV
        //0 no, 1 left/top, 2 right/bottom, 3 both
        public int ColCheck(int al, int ar, int at, int ab, int bl, int br, int bt, int bb)
        {
            int res = 0;
            if (al >= bl && al <= br) res = 10;
            if (ar >= bl && ar <= br) res += 20;
            if (ab >= bt && ab <= bb) res += 1;
            if (at >= bt && at <= bb) res += 2;
            return res;
        }
        public void ScoreUpd(int x)
        {
            if (x == 0) score = 0;
            else score += x;
            form.lblScore.Text = "SCORE: " + score;
        }
        public void SetCells(int cellNum)
        {
            this.cellNum = cellNum;
            Alien.SetCells(cellNum);
        }
        public void SetSpawnPoint(int i, bool first)
        {
            if (!first) alien[i].SetSpawnPoint(rand.Next(1, cellNum), i);
            else alien[i].SetSpawnPoint((i % (cellNum - 2)) + 1, i);
        }
        //halts all threads
        public void GameHalt()
        {
            int i;
            this.plr.Halt();
            for (i = 0; i < numAliens; i++) if (alien[i] != null) alien[i].Halt();
            for (i=0;i<alienShotNum;i++) if (alienShot[i]!=null) alienShot[i].Halt();
            for (i=0;i<bonusNum;i++) if (bonus[i]!=null) bonus[i].Halt();
            for (i=0;i<shotNum;i++) if (playerShot[i]!=null) playerShot[i].Halt();
            paused = true;
        }
        //resumes all threads
        public void GameResume()
        {
            int i;
            this.plr.UnHalt();
            for (i = 0; i < numAliens; i++) if (alien[i] != null) alien[i].UnHalt();
            for (i = 0; i < alienShotNum; i++) if (alienShot[i] != null) alienShot[i].UnHalt();
            for (i = 0; i < bonusNum; i++) if (bonus[i] != null) bonus[i].UnHalt();
            for (i = 0; i < shotNum; i++) if (playerShot[i] != null) playerShot[i].UnHalt();
            paused = false;
        }
        //kills all threads
        public void Kill()
        {
            int i;
            this.plr.Kill();
            for (i = 0; i < numAliens; i++) if (alien[i] != null) alien[i].Kill();
            for (i = 0; i < alienShotNum; i++) if (alienShot[i] != null) alienShot[i].Kill();
            for (i = 0; i < bonusNum; i++) if (bonus[i] != null) bonus[i].Kill();
            for (i = 0; i < shotNum; i++) if (playerShot[i] != null) playerShot[i].Kill();
        }
        public Level(mainForm f, int timerDelay, int alienTimerDelay, int alienMovDistance, int shotDelay, int shotNum, int shotDamage, int bonusFadeDelay, int playerMovDistance, int playerTimerDelay, int playerLives, int shotDist)
        {
            this.form = f;
            Restart(timerDelay, alienTimerDelay, alienMovDistance, shotDelay, shotNum, shotDamage, bonusFadeDelay, playerMovDistance, playerTimerDelay, playerLives, shotDist);
            /* sets and starts up timer */
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += new EventHandler(clockCount);
        }
        public void clockCount(object sender, EventArgs e)
        {
            if (timerCount == 0||paused) return;
            this.timerCount--;
            if (timerCount == 0 && plr.GetState() != 0)
            {
                plr.SetLives(-plr.GetLives());
                plr.SetState();
            }
            this.form.lblTime.Text = "Time:" + timerCount / 60 + ":" + timerCount%60;
        }
        public void Restart(int timerDelay, int alienTimerDelay, int alienMovDistance, int shotDelay, int shotNum, int shotDamage, int bonusFadeDelay, int playerMovDistance, int playerTimerDelay, int playerLives, int shotDist)
        {
            this.playerShot = new Shot[shotNum];
            Alien.SetMovDist(alienMovDistance);
            this.shotDist = shotDist;
            this.playerLives = playerLives;
            this.timerDelay = timerDelay;
            this.alienTimerDelay = alienTimerDelay;
            this.shotDelay = shotDelay;
            this.shotNum = shotNum;
            this.shotDamage = shotDamage;
            this.alienShotNum = 0;
            this.bonusFadeDelay = bonusFadeDelay;
            this.playerMovDistance = playerMovDistance;
            this.playerTimerDelay = playerTimerDelay;
            IsOver(false);
        }
        //Data setup
        void AddData(int l, int na, int ta, int atsd)
        {
            this.level = l;
            this.posMultiplier = 1 + ((l - 1) * 0.1);
            if (posMultiplier > 2) posMultiplier = 2;
            this.negMultiplier = 1 - ((l - 1) * 0.1);
            if (negMultiplier < 0) negMultiplier = 0;
            /*aliens*/
            this.totalAliens = (int)(ta * posMultiplier);
            this.timerCount = totalAliens*10;
            this.form.lblTime.Text = "Time: 00:00";
            this.oldNumAliens = this.numAliens;
            this.numAliens = (int)(na * posMultiplier);
            this.numAliveAliens = 0;
            this.tickSpawnDelay = (int)(atsd * negMultiplier);
            /*alien shots*/
            this.oldAlienShotNum = this.alienShotNum;
            this.alienShotNum = this.numAliens - 2;
            this.alienShotDelay = (int)(50 * negMultiplier);
            this.alienShotLock = 0;
            this.alienShotDamage = (int)(30 * posMultiplier);
        }
        //start new game
        public void Start(int na, int ta, int atsd)
        {
            AddData(1, na, ta, atsd);
            levelInit(true);
        }
        //start next level
        public void Start(int l, int na, int ta, int atsd)
        {
                AddData(l, na, ta, atsd);
                levelInit(false);
        }
        //initiates level
        public void levelInit(bool first)
        {
            launch = true;
            form.lblLevel.Text = "Level: " + level;
            alienShotNum = alienShotNum + 1;
            numAliveAliens = 0;
            spawnDelay = numAliens * tickSpawnDelay;
            spawnDelayCount = spawnDelay;
            alienShotLock = 0;
            if (alienShot == null) alienShot = new Shot[alienShotNum];
            if (first)
            {
                plr = new Player(form, playerTimerDelay, playerMovDistance, playerLives);//player
                alien = new Alien[numAliens];//aliens
                for (int i = 0; i < numAliens; i++)
                {
                    alien[i] = new Alien(i, form, posMultiplier, alienTimerDelay, this.shotDelay * 2, rand);
                    alien[i].SetState();
                    numAliveAliens++;
                    totalAliens--;
                }
                oldBonusNum = bonusNum;//bonuses
                bonusNum = numAliens;
                bonus = new Bonus[bonusNum];
            }
            else
            {
                //aliens restart
                Alien[] tmpAl = new Alien[numAliens];
                Alien.SetHealthMult(posMultiplier);
                Alien.SetMovDist((int)posMultiplier);
                int i = 0;
                for (i = 0; i < oldNumAliens; i++)
                {
                    SetSpawnPoint(i, true);
                    alien[i].SetState();
                    tmpAl[i] = alien[i];
                    numAliveAliens++;
                    totalAliens--;
                }
                for (; i < numAliens; i++)
                {
                    tmpAl[i] = new Alien(i, form, posMultiplier, alienTimerDelay, this.shotDelay * 2,rand);
                    tmpAl[i].SetState();
                    numAliveAliens++;
                    totalAliens--;
                }
                alien = tmpAl;
                //alien shot restart
                Shot[] tmpSh = new Shot[alienShotNum];
                for (i = 0; i < oldAlienShotNum; i++) tmpSh[i] = alienShot[i];
                alienShot = tmpSh;
                //bonus restart
                oldBonusNum = bonusNum;
                bonusNum = numAliens;
                Bonus[] tmpBn = new Bonus[bonusNum];
                for (i = 0; i < oldBonusNum; i++) tmpBn[i] = bonus[i];
                bonus = tmpBn;
            }
        }
        //game cycle; handles all the collision checks and movement choices
        public void timerCycle()
        {
            if (gameOver) return;
            if (playerWon > 0) playerWon--;
            if (playerWon < 0)
            {
                /*halts if won or lost*/
                if (plr.GetState() == 0 && plr.GetLives() == 0)
                {
                    gameOver = true;
                    form.logoPic.Visible = true;
                    form.logoPic.Image = global::AliensGame.Properties.Resources.lose;
                    form.logoPic.SetBounds((form.Width - 391) / 2, (form.Height - 100)/3, 391, 100);
                    Kill();
                    return;
                }
                if (numAliveAliens == 0)
                {
                    ScoreUpd(totalAliens * 50);//bonus points for each alien that didn't spawn
                    playerWon = 100;
                    return;
                }
                AlienCycle();
                AlienShotCycle();
            }
            if (plr.GetFire()) PlayerFire();
            AlienBonusCycle();
        }
        /* fires a shot from the top middle of the player */
        public void PlayerFire()
        {
            int i;
            if (shotLock == 0)
            {
                //single shot
                if (shotNum < 4)
                {
                    for (i = 0; i < shotNum; i++)
                    {
                        if (playerShot[i] == null)
                        {
                            playerShot[i] = new Shot(i, timerDelay, plr.GetMid(), plr.GetTop(), form, true, false, 0, rand, shotDist);
                            shotLock += shotDelay;
                            break;
                        }
                        else if (!playerShot[i].GetState())
                        {
                            playerShot[i].Init(timerDelay, plr.GetMid(), plr.GetTop(), 0, shotDist);
                            shotLock += shotDelay;
                            break;
                        }
                    }
                }
                //double shot
                if (shotNum >= 4 && shotNum < 6)
                {
                    int t=0,a=-1,b=-1;
                    for (i = 0; i < shotNum; i++)
                    {
                        if (playerShot[i] == null)
                        {
                            playerShot[i] = new Shot(i, timerDelay, plr.GetMid(), plr.GetTop(), form, true, true, 0, rand, shotDist);
                            if (t == 0) a = i;
                            if (t == 1) b = i;
                            t++;
                            if (t == 2) break;
                        }
                        else if (!playerShot[i].GetState())
                        {
                            if (t == 0) a = i;
                            if (t == 1) b = i;
                            t++;
                            if (t == 2) break;
                        }
                    }
                    if (t == 2)
                    {
                        playerShot[a].Init(timerDelay, plr.GetMid() - plr.GetWidth() / 4, plr.GetTop(), 0,shotDist);
                        playerShot[b].Init(timerDelay, plr.GetMid() + plr.GetWidth() / 4, plr.GetTop(), 0, shotDist);
                        shotLock += shotDelay;
                    }
                }
                //triple shot
                if (shotNum >= 6)
                {
                    int t = 0, a = -1, b = -1,c=-1;
                    for (i = 0; i < shotNum; i++)
                    {
                        if (playerShot[i] == null)
                        {
                            playerShot[i] = new Shot(i, timerDelay, plr.GetMid(), plr.GetTop(), form, true, true, 0, rand, shotDist);
                            if (t == 0) a = i;
                            if (t == 1) b = i;
                            if (t == 2) c = i;
                            t++;
                            if (t == 3) break;
                        }
                        else if (!playerShot[i].GetState())
                        {
                            if (t == 0) a = i;
                            if (t == 1) b = i;
                            if (t == 2) c = i;
                            t++;
                            if (t == 3) break;
                        }
                    }
                    if (t == 3)
                    {
                        playerShot[a].Init(timerDelay, plr.GetMid() - plr.GetWidth() / 4, plr.GetTop(), -1, shotDist);
                        playerShot[b].Init(timerDelay, plr.GetMid(), plr.GetTop(), 0, shotDist);
                        playerShot[c].Init(timerDelay, plr.GetMid() + plr.GetWidth() / 4, plr.GetTop(), 1, shotDist);
                        shotLock += shotDelay;
                    }
                }
            }
            else shotLock--;
        }
        /*goes through all aliens; checks for collisions, sets movements, fires, etc*/
        public void AlienCycle()
        {
            int tmp, al, ar, at, ab, bl, br, bt, bb, movDist, k;
            int xtraDist = 10;//extra padding
            bool flagLeft, flagRight;
            bool newAlien = false;
            for (int i = 0; i < numAliens; i++)
            {
                //creates one new alien if an empty slot is found
                if (alien[i].GetState() == 0)
                {
                    if (alien[i].Died())
                    {
                        alien[i].Died(false);
                        numAliveAliens--;
                    }
                    if (!newAlien)//counts down only once per cycle
                    {
                        spawnDelayCount--;
                        newAlien = true;
                    }
                    if (totalAliens > 0 && spawnDelayCount == 0 || launch)
                    {
                        spawnDelayCount = spawnDelay;//reset wait
                        SetSpawnPoint(i,false);
                        alien[i].SetState();//set alien 0 to 1
                        numAliveAliens++;
                        totalAliens--;
                    }
                    continue;
                }
                //stops if a shot killed it
                if (PlayerShotColCheck(i)) continue;
                //kills player if landed
                if (alien[i].GetLanded() && plr.GetState() != 0) plr.SetState();
                //checks for collision with player
                if (alien[i].pic.Bounds.IntersectsWith(plr.img.Bounds))
                {
                    plr.SetHealth(-alienShotDamage);
                    alien[i].SetHealth(-shotDamage);
                }
                //checks aiming and fires
                if (alien[i].GetState() == 2)
                {
                    if (alien[i].GetFire())//fire if alien wants to
                    {
                        AlienFire(alien[i].GetMid(), alien[i].GetBottom());
                        alien[i].UnSetFire();
                    }
                    //check aiming
                    if (alien[i].GetMid() >= plr.GetLeft() && alien[i].GetMid() <= plr.GetRight()) alien[i].SetCanFire(true);
                    else alien[i].SetCanFire(false);
                }
                /*  INTER-ALIEN COLLISION DETECTION AND RESOLUTION */
                flagLeft = false;
                flagRight = false;
                movDist = (alien[i].GetMov() == 1) ? -Alien.GetMovDist() : (alien[i].GetMov() == 2) ? Alien.GetMovDist() : 0;
                al = alien[i].GetLeft() - xtraDist + movDist;
                ar = alien[i].GetRight() + xtraDist + movDist;
                at = alien[i].GetTop() - xtraDist;
                ab = alien[i].GetBottom() + xtraDist;
                if (alien[i].pausedFor > 5)//if stuck
                {
                    if (!alien[i].hitLeft && !alien[i].hitRight)
                    {
                        alien[i].SetMov((alien[i].GetMov() == 1) ? 2 : 1);
                        alien[i].UnPause();
                        continue;
                    }
                    if (alien[i].pausedFor > 10)//if REALLY stuck
                    {
                        alien[i].hitLeft = false;
                        alien[i].hitRight = false;
                    }
                }
                for (k = 0; k < numAliens; k++)
                {
                    if (k != i && alien[k] != null && alien[k].GetState() == 2)
                    {
                        bl = alien[k].GetLeft();
                        br = alien[k].GetRight();
                        bt = alien[k].GetTop();
                        bb = alien[k].GetBottom();
                        tmp = ColCheck(bl, br, bt, bb, al, ar, at, ab);
                        if (tmp / 10 == 0 || tmp % 10 == 0) continue;
                        if (tmp / 10 == 3 && alien[i].GetMov() == alien[k].GetMov()) alien[i].SetMov((alien[i].GetMov() == 1)?2:1);
                        if (tmp / 10 == 1)//if i is on k's left (k to the right of i)
                        {
                            flagRight = true;
                            if (alien[i].GetMov() == 2)
                            {
                                if (alien[k].GetMov() == 2 || alien[k].Paused()) alien[i].Pause();
                                else if (alien[k].GetMov() == 1) alien[i].SetMov(1);
                            }
                        }
                        if (tmp / 10 == 2)
                        {
                            flagLeft = true;
                            if (alien[i].GetMov() == 1)
                            {
                                if (alien[k].GetMov() == 1 || alien[k].Paused()) alien[i].Pause();
                                else if (alien[k].GetMov() == 2) alien[i].SetMov(2);
                            }
                        }
                    }
                }//end of for(k)
                if (flagLeft && flagRight) alien[i].Pause();
                if (alien[i].Paused() && ((alien[i].GetMov() == 2 && !flagRight) || (alien[i].GetMov() == 1 && !flagLeft))) alien[i].UnPause();
            }//end of for(i)
            if (launch) launch = false;
        }
        //creates or recycles an alien shot
        public void AlienFire(int middle, int top)
        {
            int i;
            if (alienShotLock == 0)
            {
                for (i = 0; i < alienShotNum; i++) //finds and fills a free spot in the shot array
                {
                    if (alienShot[i] == null)
                    {
                        alienShot[i] = new Shot(i, timerDelay, middle, top, form, false, false, 0, rand, shotDist);//shot id is its place in the array
                        alienShotLock += alienShotDelay;
                        break;
                    }
                    else if (!alienShot[i].GetState())
                    {
                        alienShot[i].Init(timerDelay, middle, top, 0, shotDist);
                        alienShotLock += alienShotDelay;
                        break;
                    }
                }
            }
            else alienShotLock--;
        }
        //creates or recycles a bonus
        public void AlienBonus(int middle, int bottom)
        {
            int i;
            for (i = 0; i < bonusNum; i++) //finds and fills a free spot in the shot array
                {
                    if (bonus[i] == null)
                    {
                        bonus[i] = new Bonus(i, 5, middle, bottom, form, bonusFadeDelay,rand);
                        break;
                    }
                    if (!bonus[i].state)
                    {
                        bonus[i].Init(5, middle, bottom, bonusFadeDelay);
                        break;
                    }
                }
        }
        //goes through all active bonuses and checks for collisions
        void AlienBonusCycle()
        {
            for (int l = 0; l < bonusNum; l++)
            {
                if (bonus[l] != null && !bonus[l].adding && bonus[l].state&&bonus[l].pic.Bounds.IntersectsWith(plr.img.Bounds))
                    {
                        AlienBonusAdd(bonus[l].type);
                        bonus[l].Added();
                    }
            }
        }
        //adds Nth bonus to player
        public void AlienBonusAdd(int n)
        {
            switch (n)
            {
                case 1://100pts
                    ScoreUpd(100);
                    break;
                case 2://1up
                    plr.SetLives(1);
                    break;
                case 3://fire boost
                    if (shotNum == 9)//3 rows of 3 ought to be enough
                    {
                        shotDamage = (int)(shotDamage * 1.1);
                        break;
                    }
                    int i;
                    Shot[] tmp = new Shot[shotNum + 1];
                    for (i = 0; i < shotNum; i++) if (playerShot[i] != null) tmp[i] = playerShot[i];
                    playerShot = tmp;
                    shotNum++;
                    if (shotNum >= 4) plr.SetFireLevel(2);
                    if (shotNum >= 6) plr.SetFireLevel(3);
                    break;
                case 4://health boost
                    plr.SetHealth(25);
                    break;
                case 5://health up
                    plr.UpFullHealth(10);
                    break;
                case 6://weapon up
                    shotDamage = (int)(shotDamage * 1.1);
                    break;
                default:
                    break;
            }
        } 
        //goes through all player shots and checks for collisions
        bool PlayerShotColCheck(int i)
        {
            for (int j = 0; j < this.shotNum; j++)
            {
                if (this.playerShot[j] != null && this.playerShot[j].GetState() && (this.alien[i].GetState() > 1 && this.alien[i].GetState() < 4))
                {
                    if (this.alien[i].pic.Bounds.IntersectsWith(this.playerShot[j].pic.Bounds))
                    {
                        ScoreUpd(5); //plus 5 points if hit
                        if (this.alien[i].GetState() == 2) this.alien[i].SetHealth(-shotDamage); //update alien health
                        if (this.alien[i].GetState() == 3)
                        {
                            this.alien[i].SetState();//if alien was crashing, it will now explode
                            bonusCount++;
                            if (rand.Next(1, 100) <= bonusCount * 50)//decides if to drop a bonus
                            {
                                AlienBonus(alien[i].GetMid(), alien[i].GetBottom());
                                bonusCount = 0;
                            } 
                        }
                        this.playerShot[j].Stop(); //remove shot
                        return true;
                    }
                }
            }
            return false;
        }
        //goes through all alien shots and checks for collisions
        void AlienShotCycle()
        {
            for (int l = 0; l < alienShotNum; l++)
            {
                if (alienShot[l] != null && alienShot[l].GetState())
                {
                    if (alienShot[l].pic.Bounds.IntersectsWith(plr.img.Bounds))
                    {
                        plr.SetHealth(-alienShotDamage);
                        alienShot[l].Stop();
                    }
                }
            }
        }
        //end of func
    }
    //end of class
}
