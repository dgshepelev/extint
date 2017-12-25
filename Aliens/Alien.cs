using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace AliensGame
{
    public class Alien
    {
        private static int fullHealth = 66;
        private static double posMultiplier;
        private static int width = 88;
        private static int height = 72;
        private static int sWidth = 48;
        private static int sHeight = 56;
        private PictureBox spic;
        private Thread thrd;
        private int oldHealth = 0;
        private int dropDelay;//cycles per pixel (px)
        private int dropDelayCount;
        private int animDelay = 0;
        private int id;
        private static int alienMovDistance;//px per cycle
        private mainForm form;
        private Random rand;
        private int timerDelay;
        public PictureBox pic;
        private int left;
        private int right;
        private int top;
        private int bottom;
        private int mid;
        private int state = 0;//0 dead, 1 creating,2 active,3 crashing,4 exploding
        private bool fire = false;
        private bool canFire = false;
        private bool landed = false;
        private bool died = false;
        private int mov;//hrz movement (0,1L,2R)
        private int health = fullHealth;
        private bool pause = false;
        public static bool halt = false;
        public bool hitLeft = false;
        public bool hitRight = false;
        public int pausedFor = 0;
        private int shotDelay = 0;
        private int showDelayCount = 0;
        private static int cellNum;

        delegate void setLocationCallBack(int top);

        public bool Paused()
        {
            return pause;
        }
        public bool Died()
        {
            return died;
        }
        public void Died(bool val)
        {
            died = val;
        }
        public void Pause()
        {
            this.pause = true;
        }
        public void UnPause()
        {
            this.pause = false;
            this.pausedFor = 0;
        }
        public void Kill()
        {
            this.form.Controls.Remove(pic);
            this.form.Controls.Remove(spic); 
            thrd.Abort();
        }
        public void Halt()
        {
            halt = true;
            this.pic.Visible = false;
            if (this.state == 3 || this.state == 4) this.spic.Visible = false;
        }
        public void UnHalt()
        {
            halt = false;
            if (this.state > 0) this.pic.Visible = true;
            if (this.state == 3) this.spic.Visible = true;
        }

        public int GetState()
        {
            return state;
        }
        public void SetState()//state switch
        {
            switch (state)
            {
                case 0:
                    state++;
                    pic.Visible = true;
                    health = fullHealth;
                    SetPosData();
                    animDelay = 30;
                    this.health = fullHealth;
                    break;
                case 1:
                    state++;
                    animDelay = 31;
                    break;
                case 2:
                    state++;
                    animDelay = 31;
                    pause = false;
                    break;
                case 3:
                    state++;
                    animDelay = 0;
                    break;
                default:
                    state = 0;
                    animDelay = 0;
                    died = true;
                    break;
            }
        }
        public bool GetFire()
        {
            return fire;
        }
        public void SetCanFire(bool f)
        {
            canFire = f;
            //            fireCountDown = 0;
        }
        public void UnSetFire()
        {
            this.fire = false;
        }
        public int GetMov()
        {
            return mov;
        }
        public void SetMov(int direction)
        {
            mov = direction;
        }
        public void SetHealth(int modifier)
        {
            health += modifier;
        }
        public void SetFullHealth(int newFullHealthSize)
        {
            fullHealth = newFullHealthSize;
            health = newFullHealthSize;
        }
        public static void SetHealthMult(double mult)
        {
            posMultiplier = mult;
            fullHealth = (int)(fullHealth * posMultiplier);
            alienMovDistance = (int)(alienMovDistance * posMultiplier);
        }
        public int GetLeft()
        {
            return left;
        }
        public int GetBottom()
        {
            return bottom;
        }
        public int GetRight()
        {
            return right;
        }
        public int GetMid()
        {
            return mid;
        }
        public int GetTop()
        {
            return top;
        }
        public int GetWidth()
        {
            return width;
        }
        public int GetHeight()
        {
            return height;
        }
        public bool GetLanded()
        {
            return landed;
        }
        public static void SetMovDist(int val)
        {
            alienMovDistance = val;
        }
        public static int GetMovDist()
        {
           return alienMovDistance;
        }
        public void SetSpawnPoint(int cell, int alienId)
        {
            this.mov = rand.Next(1, 2);//moves in a random direction
            this.left = width / 2 + cell * (width + width);
            this.top = 50 + (height + 20) * (alienId / (this.form.Width / (width + width)));
            this.right = this.left + width;
            this.bottom = this.top + height;
            this.mid = this.left + width / 2;
        }
        public static void SetCells(int v)
        {
            cellNum = v;
        }
        public void SetPosData()
        {
            SetSpawnPoint((id % (cellNum - 2)) + 1, id);
            dropDelay = (this.form.Width - width - 5) / (height + 10);//5 = border distance
            if (dropDelay < 1) dropDelay = 1;
            dropDelayCount = dropDelay;
            pic.SetBounds(left, top, width, height);
        }
        public void SetPosData(int i)
        {
            SetSpawnPoint(i,i);
            dropDelay = (this.form.Width - width - 5) / (height + 10);//5 = border distance
            if (dropDelay < 1) dropDelay = 1;
            dropDelayCount = dropDelay;
            pic.SetBounds(left, top, width, height);
        }
        public Alien(int id, mainForm f, double mult, int timerDelay, int shotDelay, Random rand)
        {
            this.rand = rand;
            this.shotDelay = shotDelay;
            this.id = id;
            this.form = f;//also static
            this.timerDelay = timerDelay;//yup
            
            pic = new PictureBox();
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            SetPosData(id);

            form.Controls.Add(pic);            
            spic = new PictureBox();
            spic.SizeMode = PictureBoxSizeMode.Zoom;
            spic.Visible = false;
            spic.Image = global::AliensGame.Properties.Resources.smoke;
            spic.SetBounds(-width, -height, width, height);
            form.Controls.Add(spic);
            thrd = new Thread(new ThreadStart(AlienThreadStart));
            thrd.Start();
        }
        private void AlienThreadStart()
        {
            while (true)
            {
                AlienCycle(0);
                Thread.Sleep(timerDelay);
            }
        }
        private void AlienCycle(int var)
        {
            if (form.InvokeRequired)
            {
                setLocationCallBack d = new setLocationCallBack(AlienCycle);
                form.Invoke(d, new object[] { var });
            }
            else
            {
                if (state == 0 || landed || halt) return;//skip all this nonsense if dead
                if (oldHealth != health)//if health has changed
                {
                    if (health <= 0 && state == 2) SetState();
                    oldHealth = health;
                }
                switch (state)
                {
                    case 1://create
                        create();
                        break;
                    case 2://active
                        if (this.showDelayCount > 0) this.showDelayCount--;
                        if (this.canFire && this.showDelayCount == 0 && rand.Next(1, 100) > 50) fire = true;
                        animDelay--;
                        if (animDelay == 30)
                        {
                            if (health == fullHealth) this.pic.Image = global::AliensGame.Properties.Resources.aaa;
                            else this.pic.Image = global::AliensGame.Properties.Resources.aac;
                        }
                        if (animDelay <= 0)
                        {
                            if (health == fullHealth) this.pic.Image = global::AliensGame.Properties.Resources.aab;
                            else this.pic.Image = global::AliensGame.Properties.Resources.aad;
                            animDelay = 60;
                        }
                        if (!pause) move(1);
                        else this.pausedFor++;
                        break;
                    case 3://crash
                        crash();
                        break;
                    case 4://explode
                        die();
                        break;
                    default:
                        break;
                }
            }
        }
        //movement thing
        private void move(int drop)
        {
            //horizontal movement
            if (mov == 1)
            {
                if (this.left - alienMovDistance > 5 || this.state == 3)
                {
                    this.pic.Left -= alienMovDistance;
                    this.left -= alienMovDistance;
                    this.right -= alienMovDistance;
                    this.mid -= alienMovDistance;
                    this.hitLeft = false;
                }
                else if (left - alienMovDistance <= 5)
                {
                    this.mov = 2;//reverse direction
                    this.hitLeft = true;
                }
            }
            if (mov == 2)
            {
                if (this.right + alienMovDistance < form.Width - 5 || this.state == 3)
                {
                    this.pic.Left += alienMovDistance;
                    this.left += alienMovDistance;
                    this.right += alienMovDistance;
                    this.mid += alienMovDistance;
                    this.hitRight = false;
                }
                else if (this.right + alienMovDistance >= form.Width - 5)
                {
                    this.mov = 1;//reverse direction
                    this.hitRight = true;
                }
            }
            if (this.bottom + 1 < form.Height - height + 40)//if not landed
            {
                if (state == 2)
                {//glide
                    dropDelayCount--;
                    if (dropDelayCount == 0)
                    {
                        this.pic.Top += drop;
                        this.top += drop;
                        this.bottom += drop;
                        this.dropDelayCount = dropDelay;
                    }
                }
                else//fall
                {
                    this.pic.Top += drop;
                    top += drop;
                    bottom += drop;
                }
            }
            else//landed: win if alive, die if crashed
            {
                if (state == 2) landed = true;
                else SetState();
            }
        }
        //moves and animates
        private void crash()
        {
            spic.Left = mid - sWidth;
            spic.Top = top - sHeight;
                animDelay--;
                if (animDelay == 30)
                {
                    this.pic.Image = global::AliensGame.Properties.Resources.aae;
                    spic.Image = global::AliensGame.Properties.Resources.smoke;
                }
                if (animDelay <= 0)
                {
                    this.pic.Image = global::AliensGame.Properties.Resources.aaf;
                    spic.Image = global::AliensGame.Properties.Resources.smoke2;
                    animDelay = 60;
                }
                spic.Visible = true;
                move(5);
        }
        //reverse explodes
        private void create()
        {
            if (animDelay == 30) pic.Image = global::AliensGame.Properties.Resources.ca;
            if (animDelay == 20) pic.Image = global::AliensGame.Properties.Resources.cb;
            if (animDelay == 10) pic.Image = global::AliensGame.Properties.Resources.cc;
            if (animDelay == 1)
            {
                pic.Image = global::AliensGame.Properties.Resources.aaa;
                SetState();
                return;
            }
            animDelay--;
        }
        //explodes
        private void die()
        {
            if (animDelay == 0)
            {
                pic.Image = global::AliensGame.Properties.Resources.bma;
                spic.Image = global::AliensGame.Properties.Resources.smoke3;
            }
            if (animDelay == 10)
            {
                pic.Image = global::AliensGame.Properties.Resources.bmb;
                spic.Image = global::AliensGame.Properties.Resources.smoke4;
            }
            if (animDelay == 20)
            {
                pic.Image = global::AliensGame.Properties.Resources.bmc;
                spic.Image = global::AliensGame.Properties.Resources.smoke5;
            }
            if (animDelay == 30)
            {
                pic.Visible = false;
                spic.Visible = false;
                SetState();
                return;
            }
            animDelay++;
        }
        //end of func
    }
    //end of class
}