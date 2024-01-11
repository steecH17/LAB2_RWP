using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2_RWP
{
    internal class Rain
    {
        public float posX;
        public float posY;
        public float startPosX;
        float speed = 1;
        public float[] size;
        float[] heightSize = {5f, 10f };
        float widthSize = 3;
        public int windDirection = 0;//0-обычный дождь, 1-дождь идет под углом вправо, -1 идет влево
        

        public Rain(float x, float y, float speed, int ind) 
        {
            this.posX = x;
            this.posY = y;
            this.startPosX = x;
            this.speed = speed;
            this.size = new float[2];
            this.size[0] = this.widthSize;
            this.size[1] = this.heightSize[ind];
            
        }
        public void NewParticlesPosition(float maxY)
        {
            if((this.posY + this.speed + this.size[1]) >= maxY)
            {
                this.posY = 0;
                if(windDirection != 0) { this.posX = this.startPosX; }
            }
            else
            {
                this.posY += this.speed;
            }
        }
        public void SetWindPosX()
        {
            if (windDirection == -1)
            {
                this.posX -= this.speed;
            }
            if (windDirection == 1)
            {
                this.posX += this.speed;
            }
        }
        public void SetWind(int windType) 
        {
            this.windDirection = windType;
        }
    }
}
