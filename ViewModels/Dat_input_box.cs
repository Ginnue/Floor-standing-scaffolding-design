using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Floor_standing_scaffolding_design_software.ViewModels
{
    class Dat_input_box
    {
        //限制非法字符输入，只允许输入数字和小数点
        public static void TextBox_PreviewTextInput(object sender,
            TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            //这是对中文值的判断
            if (textBox == null) return;
            Regex numbeRegex = new Regex("^[-]?\\d+[.]?\\d*$");
            Regex zeroRegex = new Regex("^[0]+[0-9]+$");
            e.Handled =
                    !numbeRegex.IsMatch(
                        textBox.Text.Insert(
                            textBox.SelectionStart, e.Text))
                            || zeroRegex.IsMatch(
                        textBox.Text.Insert(
                            textBox.SelectionStart, e.Text));
            textBox.Text = textBox.Text.Trim();
        }
        /// <summary>
        /// 键盘输入值事件(可以小键盘上的数字键和英文输入状态下的句点键盘)
        /// </summary>
        /// <param name="e"></param>
        public static void TextBox_OnPreviewKeyDown(KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 &&
                 e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
                ||
                (e.Key >= Key.D0 && e.Key <= Key.D9 &&
                 e.KeyboardDevice.Modifiers != ModifierKeys.Shift) ||
                e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right ||
                e.Key == Key.Enter || e.Key == Key.Decimal ||
                e.Key == Key.OemPeriod)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        ///  禁止粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumberic(text))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }
        public static bool isNumberic(string _string)
        {
            if (string.IsNullOrEmpty(_string))
                return false;
            foreach (char c in _string)
            {
                if (!char.IsDigit(c))
                    //if(c<'0' c="">'9')//最好的方法,在下面测试数据中再加一个0，然后这种方法效率会搞10毫秒左右
                    return false;
            }
            return true;
        }
    }
}
