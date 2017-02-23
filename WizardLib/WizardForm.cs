using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace WizardLib {

	/// <summary>
	/// Used to identify the various buttons that may appear within a wizard
	/// dialog.  
	/// </summary>
	[Flags]
	public enum WizardButton {
		/// <summary>
		/// Identifies the <b>Back</b> button.
		/// </summary>
		Back = 0x00000001,

		/// <summary>
		/// Identifies the <b>Next</b> button.
		/// </summary>
		Next = 0x00000002,

		/// <summary>
		/// Identifies the <b>Finish</b> button.
		/// </summary>
		Finish = 0x00000004,

		/// <summary>
		/// Identifies the disabled <b>Finish</b> button.
		/// </summary>
		DisabledFinish = 0x00000008,

		/// <summary>
		/// Identifies the <b>DisabledAll</b> button.
		/// </summary>
		DisabledAll = 0x000000016,
	}

	
	public partial class WizardForm : Form {

		// ==================================================================
		// Public Constants
		// ==================================================================

		/// <summary>
		/// Used by a page to indicate to this wizard that the next page
		/// should be activated when either the Back or Next buttons are
		/// pressed.
		/// </summary>
		public const string NextPage = "";

		/// <summary>
		/// Used by a page to indicate to this wizard that the selected page
		/// should remain active when either the Back or Next buttons are
		/// pressed.
		/// </summary>
		public const string NoPageChange = null;


		// ==================================================================
		// Private Fields
		// ==================================================================

		/// <summary>
		/// Array of wizard pages.
		/// </summary>
		public ArrayList m_pages = new ArrayList();

		/// <summary>
		/// Index of the selected page; -1 if no page selected.
		/// </summary>
		public int m_selectedIndex = -1;


		// ==================================================================
		// Protected Fields
		// ==================================================================

		/// <summary>
		/// The Back button.
		/// </summary>
		protected Button m_backButton;

		/// <summary>
		/// The Next button.
		/// </summary>
		protected Button m_nextButton;

		/// <summary>
		/// The Cancel button.
		/// </summary>
		protected Button m_cancelButton;

		/// <summary>
		/// The Finish button.
		/// </summary>
		protected Button m_finishButton;
		
		public WizardForm() {
			InitializeComponent();

			// Ensure Finish and Next buttons are positioned similarly
			m_finishButton.Location = m_nextButton.Location;
		}


		/// <summary>
		/// Activates the page at the specified index in the page array.
		/// </summary>
		/// <param name="newIndex">
		/// Index of new page to be selected.
		/// </param>
		public void ActivatePage(int newIndex) {
			// Ensure the index is valid
			if (newIndex < 0 || newIndex >= m_pages.Count)
				throw new ArgumentOutOfRangeException();

			// Deactivate the current page if applicable
			WizardPage currentPage = null;
			if (m_selectedIndex != -1) {
				currentPage = (WizardPage)m_pages[m_selectedIndex];
				if (!currentPage.OnKillActive())
					return;
			}

			//if (newIndex != m_selectedIndex + 1) {
			//    return;
			//}

			// Activate the new page
			WizardPage newPage = (WizardPage)m_pages[newIndex];
			if (!newPage.OnSetActive())
				return;

			// Update state
			m_selectedIndex = newIndex;
			if (currentPage != null)
				currentPage.Visible = false;
			newPage.Visible = true;
			newPage.Focus();
		}

		/// <summary>
		/// Handles the Click event for the Back button.
		/// </summary>
		private void OnClickBack(object sender, EventArgs e) {
			// Ensure a page is currently selected
			if (m_selectedIndex != -1) {
				// Inform selected page that the Back button was clicked
				string pageName = ((WizardPage)m_pages[
					m_selectedIndex]).OnWizardBack();
				switch (pageName) {
					// Do nothing
					case NoPageChange:
						break;

					// Activate the next appropriate page
					case NextPage:
						if (m_selectedIndex - 1 >= 0)
							ActivatePage(m_selectedIndex - 1);
						break;

					// Activate the specified page if it exists
					default:
						foreach (WizardPage page in m_pages) {
							if (page.Name == pageName)
								ActivatePage(m_pages.IndexOf(page));
						}
						break;
				}
			}
		}

		/// <summary>
		/// Handles the Click event for the Cancel button.
		/// </summary>
		private void OnClickCancel(object sender, EventArgs e) {
			// Close wizard
			//DialogResult = DialogResult.Cancel;
			if (MessageBox.Show("是否确定取消安装?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
				Application.Exit();
			}
		}

		/// <summary>
		/// Handles the Click event for the Finish button.
		/// </summary>
		private void OnClickFinish(object sender, EventArgs e) {
			// Ensure a page is currently selected
			if (m_selectedIndex != -1) {
				// Inform selected page that the Finish button was clicked
				WizardPage page = (WizardPage)m_pages[m_selectedIndex];
				if (page.OnWizardFinish()) {
					// Deactivate page and close wizard
					if (page.OnKillActive())
						//DialogResult = DialogResult.OK;
						Application.Exit();
				}
			}
		}

		/// <summary>
		/// Handles the Click event for the Next button.
		/// </summary>
		private void OnClickNext(object sender, EventArgs e) {
			// Ensure a page is currently selected
			if (m_selectedIndex != -1) {
				// Inform selected page that the Next button was clicked
				string pageName = ((WizardPage)m_pages[
					m_selectedIndex]).OnWizardNext();
				switch (pageName) {
					// Do nothing
					case NoPageChange:
						break;

					// Activate the next appropriate page
					case NextPage:
						if (m_selectedIndex + 1 < m_pages.Count)
							ActivatePage(m_selectedIndex + 1);
						break;

					// Activate the specified page if it exists
					default:
						foreach (WizardPage page in m_pages) {
							if (page.Name == pageName)
								ActivatePage(m_pages.IndexOf(page));
						}
						break;
				}
			}
		}


		// ==================================================================
		// Protected Methods
		// ==================================================================

		/// <seealso cref="System.Windows.Forms.Control.OnControlAdded">
		/// System.Windows.Forms.Control.OnControlAdded
		/// </seealso>
		protected override void OnControlAdded(ControlEventArgs e) {
			// Invoke base class implementation
			base.OnControlAdded(e);

			// Set default properties for all WizardPage instances added to
			// this form
			WizardPage page = e.Control as WizardPage;
			if (page != null) {
				page.Visible = false;
				page.Location = new Point(0, 0);
				page.Size = new Size(Width, 390);
				m_pages.Add(page);
				if (m_selectedIndex == -1)
					m_selectedIndex = 0;
			}
		}

		/// <seealso cref="System.Windows.Forms.Form.OnLoad">
		/// System.Windows.Forms.Form.OnLoad
		/// </seealso>
		protected override void OnLoad(EventArgs e) {
			// Invoke base class implementation
			base.OnLoad(e);

			// Activate the first page in the wizard
			if (m_pages.Count > 0)
				ActivatePage(0);
		}


		// ==================================================================
		// Public Methods
		// ==================================================================

		/// <summary>
		/// Sets the text in the Finish button.
		/// </summary>
		/// <param name="text">
		/// Text to be displayed on the Finish button.
		/// </param>
		public void SetFinishText(string text) {
			// Set the Finish button text
			m_finishButton.Text = text;
		}

		/// <summary>
		/// Enables or disables the Back, Next, or Finish buttons in the
		/// wizard.
		/// </summary>
		/// <param name="flags">
		/// A set of flags that customize the function and appearance of the
		/// wizard buttons.  This parameter can be a combination of any
		/// value in the <c>WizardButton</c> enumeration.
		/// </param>
		/// <remarks>
		/// Typically, you should call <c>SetWizardButtons</c> from
		/// <c>WizardPage.OnSetActive</c>.  You can display a Finish or a
		/// Next button at one time, but not both.
		/// </remarks>
		public void SetWizardButtons(WizardButton flags) {
			// Enable/disable and show/hide buttons appropriately
			m_backButton.Enabled =
				(flags & WizardButton.Back) == WizardButton.Back;
			m_backButton.Visible =
				(flags & WizardButton.Back) == WizardButton.Back;
			m_nextButton.Enabled =
				(flags & WizardButton.Next) == WizardButton.Next;
			m_nextButton.Visible =
				(flags & WizardButton.Finish) == 0 &&
				(flags & WizardButton.DisabledFinish) == 0;
			m_finishButton.Enabled =
				(flags & WizardButton.DisabledFinish) == 0;
			m_finishButton.Visible =
				(flags & WizardButton.Finish) == WizardButton.Finish ||
				(flags & WizardButton.DisabledFinish) == WizardButton.DisabledFinish;

			m_cancelButton.Enabled = (m_backButton.Enabled || m_nextButton.Enabled);
			m_cancelButton.Visible = (!m_finishButton.Visible || !m_finishButton.Enabled);

			if (flags == WizardButton.DisabledAll) {
				m_backButton.Visible = m_nextButton.Visible = m_cancelButton.Visible = true;
				m_backButton.Enabled = m_nextButton.Enabled = m_cancelButton.Enabled = false;
				m_finishButton.Visible = m_finishButton.Enabled = false;
			}

			// Set the AcceptButton depending on whether or not the Finish
			// button is visible or not

			//AcceptButton = m_finishButton.Visible ? m_finishButton : m_nextButton;
		}


	}
}
