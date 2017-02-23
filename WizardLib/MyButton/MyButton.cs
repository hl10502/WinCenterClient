using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

/*
 * 按钮按下去时，还是去出现黑线框，这个黑线框通过属性设置时无法去掉的，需要自定义按钮，然后重绘。
 * 自定义了一个Button。点击Button时不会出现黑色边框
 *
 */
namespace WizardLib {
	class MyButton : Button {
		public MyButton() {
		}

		protected override bool ShowFocusCues {
			get {
				return false;
			}
		}
	}
}
