using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WCT
{
    class Scramble
    {
        private static int SCRAMBLE_SIZE = 25;
        private static char[] CUBE_FACES = new char[] 
        { 'F', 'B', 'L', 'R', 'U', 'D' };

        public string scramble_gen()
        {
            int move_count = 0;
            string scramble="";
            string previous = "";
            string selected_move = "";
            while(move_count < SCRAMBLE_SIZE)
            {
                selected_move = select_side(previous);
                scramble = scramble.Insert(scramble.Length,selected_move + " ");
                previous = selected_move;
                move_count++;
            }
            return scramble;
        }

        private string select_side(string previous_side)
        {
            string selection = "";
            Random r = new Random();
            bool invalid = true;

            // select a side and make sure it's not a repeat
            while (invalid) 
            {
                invalid = false;
                selection = CUBE_FACES[r.Next(6)].ToString();

                //do a dice roll and see if you'll make it double or inverse
                if (r.Next(6) == 0)
                {
                    selection = selection.Insert(selection.Length, "2");
                }
                else if (r.Next(6) == 0)
                {
                    selection = selection.Insert(selection.Length, "'");
                }

                Debug.Print("sel: " + "`"+selection +"`"+ " vs prev: " + "`"+previous_side+ "`");
                if (selection == previous_side)
                    invalid = true;
                if (selection.Substring(0,1) == previous_side)
                    invalid = true;
                if (invalid) Debug.Print("だめ");
            }

            

            return selection;

        }

       

    
}
