using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Word;

namespace BookStore
{
    class SelectData
    {
        public List<Book> selectedBooks = new List<Book>(100);
        DataAccess dataConnection = MainWindow.dataConnection;
        string filePath; // path to Word template file
        Microsoft.Office.Interop.Word.Application wordApplication;
        Microsoft.Office.Interop.Word.Document wordDocument;
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
        public void writeData(Book cheepestBook,List<Book> selectedByAgeBooks)
        {
            filePath = Environment.CurrentDirectory.ToString();
            try
            {
                wordApplication = new Microsoft.Office.Interop.Word.Application();
                wordDocument = wordApplication.Documents.Add(filePath + "\\Результати_пошуку_книжок.docx");
                wordApplication.Visible = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + char.ConvertFromUtf32(13) + "Шаблон не знайдено" + char.ConvertFromUtf32(13) +
                    "Помістіть шаблон Результати_пошуку_книжок.dotx у каталог із exe програми","Помилка!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            replaceX_YText(MainWindow.selectedAgeRange,"<X-Y>");
            replaceCheepestBookTable(cheepestBook,1);
            replaceSelectedBooks(selectedBooks, 2);
            try
            {
                wordDocument.Save();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message + "Помилка збереження відібраних книжок", "Помилка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void replaceX_YText(string textToReplace,string replacedText)
        {
            object missing = Type.Missing;
            Microsoft.Office.Interop.Word.Range selText;
            selText = wordDocument.Range(wordDocument.Content.Start, wordDocument.Content.End);
            Microsoft.Office.Interop.Word.Find find = wordApplication.Selection.Find;
            find.Text = replacedText;
            find.Replacement.Text = textToReplace;
            Object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
            Object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

            find.Execute(FindText: Type.Missing,
                MatchCase: false,
                MatchWholeWord: false,
                MatchWildcards: false,
                MatchSoundsLike: false,
                MatchAllWordForms: false,
                Forward: true,
                Wrap:wrap,
                Format:false,
                ReplaceWith:missing,Replace:replace
                );
        }
        private void replaceSelectedBooks(List<Book> selectedBooks,int tableNum)
        {
            for (int i = 0; i < selectedBooks.Count; i++)
            {
                wordDocument.Tables[tableNum].Rows.Add();
                wordDocument.Tables[tableNum].Cell(2 + i, 1).Range.Text = selectedBooks[i].name;
                wordDocument.Tables[tableNum].Cell(2 + i, 2).Range.Text = selectedBooks[i].quantity.ToString();
                wordDocument.Tables[tableNum].Cell(2 + i, 3).Range.Text = selectedBooks[i].ageRange;
                wordDocument.Tables[tableNum].Cell(2 + i, 4).Range.Text = selectedBooks[i].price.ToString();
            }
        }
        private void replaceCheepestBookTable(Book cheepestBook,int tableNum)
        {
            wordDocument.Tables[tableNum].Cell(2, 1).Range.Text = cheepestBook.name;
            wordDocument.Tables[tableNum].Cell(2, 2).Range.Text = cheepestBook.quantity.ToString();
            wordDocument.Tables[tableNum].Cell(2, 3).Range.Text = cheepestBook.ageRange;
            wordDocument.Tables[tableNum].Cell(2, 4).Range.Text = cheepestBook.price.ToString();
        }
        ~SelectData()
        {
            if (wordDocument != null)
            {
                wordDocument.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdPromptToSaveChanges);
            }
            if (wordApplication != null)
            {
                wordApplication.Quit(Microsoft.Office.Interop.Word.WdSaveOptions.wdPromptToSaveChanges);
            }
        }
    }
}
