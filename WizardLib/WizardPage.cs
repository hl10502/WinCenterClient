﻿using System.Windows.Forms;

namespace WizardLib {
	public partial class WizardPage : UserControl {
		public WizardPage() {
			InitializeComponent();
		}


		// ==================================================================
		// Protected Properties
		// ==================================================================

		/// <summary>
		/// Gets the <see cref="WizardForm">WizardForm</see>
		/// to which this <see cref="WizardPage">WizardPage</see>
		/// belongs.
		/// </summary>
		protected WizardForm Wizard {
			get {
				// Return the parent WizardForm
				return (WizardForm)Parent;
			}
		}


		// ==================================================================
		// Protected Internal Methods
		// ==================================================================

		/// <summary>
		/// Called when the page is no longer the active page.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the page was successfully deactivated; otherwise
		/// <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Override this method to perform special data validation tasks.
		/// </remarks>
		protected internal virtual bool OnKillActive() {
			// Deactivate if validation successful
			return Validate();
		}

		/// <summary>
		/// Called when the page becomes the active page.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the page was successfully set active; otherwise
		/// <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Override this method to performs tasks when a page is activated.
		/// Your override of this method should call the default version
		/// before any other processing is done.
		/// </remarks>
		protected internal virtual bool OnSetActive() {
			// Activate the page
			return true;
		}

		/// <summary>
		/// Called when the user clicks the Back button in a wizard.
		/// </summary>
		/// <returns>
		/// <c>WizardForm.DefaultPage</c> to automatically advance to the
		/// next page; <c>WizardForm.NoPageChange</c> to prevent the page
		/// changing.  To jump to a page other than the next one, return
		/// the <c>Name</c> of the page to be displayed.
		/// </returns>
		/// <remarks>
		/// Override this method to specify some action the user must take
		/// when the Back button is pressed.
		/// </remarks>
		protected internal virtual string OnWizardBack() {
			// Move to the default previous page in the wizard
			return WizardForm.NextPage;
		}

		/// <summary>
		/// Called when the user clicks the Finish button in a wizard.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the wizard finishes successfully; otherwise
		/// <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Override this method to specify some action the user must take
		/// when the Finish button is pressed.  Return <c>false</c> to
		/// prevent the wizard from finishing.
		/// </remarks>
		protected internal virtual bool OnWizardFinish() {
			// Finish the wizard
			return true;
		}

		/// <summary>
		/// Called when the user clicks the Next button in a wizard.
		/// </summary>
		/// <returns>
		/// <c>WizardForm.DefaultPage</c> to automatically advance to the
		/// next page; <c>WizardForm.NoPageChange</c> to prevent the page
		/// changing.  To jump to a page other than the next one, return
		/// the <c>Name</c> of the page to be displayed.
		/// </returns>
		/// <remarks>
		/// Override this method to specify some action the user must take
		/// when the Next button is pressed.
		/// </remarks>
		protected internal virtual string OnWizardNext() {
			// Move to the default next page in the wizard
			return WizardForm.NextPage;
		}

	}
}
