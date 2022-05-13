using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class SelectData
    {
        public List<Book> selectedBooks = new List<Book>();
        DataAccess dataConnection = MainWindow.dataConnection;
        bool isInAgeRange(int givenLower,int givenHigher,int thisLower,int thisHigher)
        {
            if (thisLower >= givenLower && thisLower <= givenHigher && thisHigher >= givenLower  &&  thisHigher <= givenHigher)
            {
                return true;
            }
            return false;
        }
        public void selectBooksByAge(int lowAge,int highAge)
        {
            for (int i = 0; i < dataConnection.booksToShowList.Count; i++)
            {
                string [] thisAgeRange = dataConnection.booksToShowList[i].ageRange.Split('-');
                int thisLowAge = Convert.ToInt32(thisAgeRange[0]);
                int thisHighAge = Convert.ToInt32(thisAgeRange[1]);
                if (isInAgeRange(lowAge,highAge,thisLowAge,thisHighAge))
                {
                    selectedBooks.Add(dataConnection.booksToShowList[i]);
                }
            }
        }
        
    }
}
