using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBreakdownRegisterV3.Utility
{
    public class SetCellBackColor
    {
        public DataGridView dataGridView;
        public string columnName;
        public List<Color> fadeColors;
        DataGridViewCellStyle style;


        public SetCellBackColor(DataGridView dataGridView, string columnName, List<Color> fadeColors)
        {
            this.dataGridView = dataGridView;
            this.columnName = columnName;
            this.fadeColors = fadeColors;
        }

        public void Set()
        {

            //get the index of the column with the name columnName
            int index = dataGridView.Columns["SeverityMaintenance"].Index;
            //update each back color of the cells of the datagrid in the pretended column.
            //loop each row
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                //loop each cell in row
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    //if cell is in the right column
                    if (row.Cells[i].ColumnIndex == index)
                    {
                        //get the actual value of the cell
                        int actualValue = int.Parse(row.Cells[i].Value.ToString());
                        //set the back color of the cell based on the fadeColors list
                        DataGridViewCellStyle cellStyle = row.Cells[i].Style;
                        cellStyle.ApplyStyle(getStyle(actualValue));
                        //cellStyle.SelectionBackColor = fadeColors[actualValue - 1];
                        //set the text color to black when cell is selected
                        //cellStyle.SelectionForeColor = Color.Black;
                    }
                }
            }
        }

        private DataGridViewCellStyle getStyle(int severity)
        {
            style = new DataGridViewCellStyle() { 
                BackColor = Color.FromArgb(fadeColors[severity - 1].ToArgb()),
                SelectionBackColor = Color.FromArgb(fadeColors[severity - 1].ToArgb()),
                SelectionForeColor = Color.Black 
            };
            return style;
        }
    }
}
