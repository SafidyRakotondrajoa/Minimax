using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Node
    {
        public static int X_Mask = 0x15555;
        public static int O_Mask = 0x2AAAA;
        public static int Turn_Mask = 0x80000;// 1<19
        public static int Mask_All = 0x3FFFF;
        public static int L1_Mask = 0x0003F;
        public static int L2_Mask = 0x00FC0;
        public static int L3_Mask = 0x3F000;
        public static int V1_MAsk = 0x030C3;
        public static int V2_MAsk = 0x0C30C;
        public static int V3_MAsk = 0x30C30;
        public static int D1_Mask = 0x30303;
        public static int D2_Mask = 0x03330;

        public static int[] Lines = { L1_Mask, L2_Mask, L3_Mask, V1_MAsk, V2_MAsk, V3_MAsk, D1_Mask, D2_Mask };

        private int state;
        
        public Node BestSucc { get; set; }

        public Node()
        {
            state = 0;
            BestSucc = null;
        }

        public Node(int x)
        {
            state = x;
            BestSucc = null;
        }
        //get if full
        public bool IsFull()
        {
            return (((state & O_Mask) >> 1) | (state & X_Mask))==X_Mask;
        }

        public int GetTurn()
        {
            if ((state & (Turn_Mask)) == 0) return 1;
            return -1;
        }

        //setting a node

        public void SetState(int x, int y)
        {
            int decal = (x + 3 * y) * 2;
            int pos = 3 << decal;
            this.state = state ^ (Turn_Mask) | pos;
        }

        // getting a node state

        public int GetState(int x, int y)
        {
            int decal = (x + 3 * y) * 2;
            int pos = 3 << decal;
            int result = (state & pos) >> decal;
            return (result + 1) % 3 - 1; //0 1 -1
        }
        //get successor
        public List<Node> GetSucc()
        {
            List<Node> Output = new List<Node>();
            if (IsFull()) return Output;
            int newVal = (state & (Turn_Mask))==0?1:2;
            int mask = 3;
            for(int i = 0; i < 9; i++)
            {
                if ((state & (mask)) == 0)
                {
                    Output.Add(new Node(state ^(Turn_Mask)|(newVal)));
                }
                mask <<= 2;
                newVal <<= 2;
            }
            return Output;

        }

        public bool IsClickable(int i, int j)
        {
            return GetState(i, j) ==0;
        }

        public Node DoClick(int sourceX,int sourceY)
        {
            if (!IsClickable(sourceX, sourceY)) return null;
            int decal = (sourceX + 3 * sourceY) * 2;
            int newVal = (state & (Turn_Mask)) == 0 ? 1 : 2;
            newVal <<= decal;
            return new Node(state ^ (Turn_Mask) | (newVal));
        }

        public int PerfectEval (int pos)
        {
            int us = pos == 1 ? X_Mask : O_Mask;
            int them = pos != 1 ? X_Mask : O_Mask;
            for (int i =0;i<Lines.Length;i++)
            {
                int mask = Lines[i];
                if ((mask & state) == (mask & us)) return 100;
                if ((mask & state) == (mask & them)) return -100;

            }
            return 0;
        }

        public bool End()
        {
            return (PerfectEval(state) != 0) || (IsFull());
        }
    }
}
