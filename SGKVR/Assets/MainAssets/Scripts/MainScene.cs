using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace VRKeys {

	/// <summary>
	/// Example use of VRKeys keyboard.
	/// </summary>
	public class MainScene : MonoBehaviour {

		/// <summary>
		/// Reference to the VRKeys keyboard.
		/// </summary>
		public Keyboard keyboard;

		public GameObject camera;

		[SerializeField]
		private bool state1, state2, state3;

		/// <summary>
		/// See the following for why this is so convoluted:
		/// http://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/EmailAddressAttribute.cs,54
		/// http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx/
		/// </summary>
		private Regex emailValidator = new Regex (@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", RegexOptions.IgnoreCase);

		private GameManager m_GameManager;
		private User CurrentUser;

		[SerializeField]
		private GameObject RegPanel;

		private void Awake()
        {
			GameObject GameManager = GameObject.Find("GameManager");
			m_GameManager = GameManager.GetComponent<GameManager>();
			CurrentUser = m_GameManager.CurrentUser;
			RegPanel.SetActive(true);
			state1 = true;
		}

		public void btnRegCancel()
        {
			RegPanel.SetActive(false);
		}

		public void btnRegOn()
		{
			RegPanel.SetActive(false);
			state1 = true;
			keyboard.Enable();

			if (state1)
			{
				keyboard.SetPlaceholderMessage("Пожалуйста, введите ваш Email");
			}
			if (state2)
			{
				keyboard.SetPlaceholderMessage("Пожалуйста, введите никнейм");
			}
			if (state3)
			{
				keyboard.SetPlaceholderMessage("Пожалуйста, введите пароль");
			}

			keyboard.OnUpdate.AddListener(HandleUpdate);
			keyboard.OnSubmit.AddListener(HandleSubmit);
			keyboard.OnCancel.AddListener(HandleCancel);
		}


		/// <summary>
		/// Show the keyboard with a custom input message. Attaching events dynamically,
		/// but you can also use the inspector.
		/// </summary>
		private void OnEnable () {

			// Automatically creating camera here to show how
			//GameObject camera = new GameObject ("Main Camera");
			Camera cam = camera.GetComponent<Camera> ();
			cam.nearClipPlane = 0.1f;
			camera.AddComponent<AudioListener> ();

			// Improves event system performance
			Canvas canvas = keyboard.canvas.GetComponent<Canvas> ();
			canvas.worldCamera = cam;
		}

		private void OnDisable () {
			keyboard.OnUpdate.RemoveListener (HandleUpdate);
			keyboard.OnSubmit.RemoveListener (HandleSubmit);
			keyboard.OnCancel.RemoveListener (HandleCancel);

			keyboard.Disable ();
		}

		/// <summary>
		/// Press space to show/hide the keyboard.
		///
		/// Press Q for Qwerty keyboard, D for Dvorak keyboard, and F for French keyboard.
		/// </summary>
		private void Update () {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (keyboard.disabled) {
					keyboard.Enable ();
				} else {
					keyboard.Disable ();
				}
			}

			if (keyboard.disabled) {
				return;
			}

			//if (Input.GetKeyDown (KeyCode.Q)) {
			//	keyboard.SetLayout (KeyboardLayout.Qwerty);
			//} else if (Input.GetKeyDown (KeyCode.F)) {
			//	keyboard.SetLayout (KeyboardLayout.French);
			//} else if (Input.GetKeyDown (KeyCode.D)) {
			//	keyboard.SetLayout (KeyboardLayout.Dvorak);
			//}
		}

		/// <summary>
		/// Hide the validation message on update. Connect this to OnUpdate.
		/// </summary>
		public void HandleUpdate (string text) {
			keyboard.HideValidationMessage ();
		}

		/// <summary>
		/// Validate the email and simulate a form submission. Connect this to OnSubmit.
		/// </summary>
		public void HandleSubmit (string text) {
			keyboard.DisableInput ();

			if (state1)
            {
				if (!ValidateEmail(text))
				{
					keyboard.ShowValidationMessage("Пожалуйста, введите действительный Email");
					keyboard.EnableInput();
					return;
				}

				StartCoroutine(SubmitEmail(text));
				state1 = false;
			}
			if (state2)
			{
				StartCoroutine(SubmitNick(text));
				keyboard.SetPlaceholderMessage("Пожалуйста, введите никнейм");
				state2 = false;
			}
			if (state3)
			{
				StartCoroutine(SubmitPass(text));
				keyboard.SetPlaceholderMessage("Пожалуйста, введите пароль");
				state3 = false;
			}
		}

		public void HandleCancel () {
			Debug.Log ("Отменен ввод с клавиатуры!");
		}

		/// <summary>
		/// Pretend to submit the email before resetting.
		/// </summary>
		private IEnumerator SubmitEmail (string email) {
			state1 = false;
			keyboard.ShowInfoMessage ("Код подтверждения отправлен на ваш Email... ;)");

			yield return new WaitForSeconds (2f);

			keyboard.ShowSuccessMessage ("Ваш Email " + email + " подтвержден");

			CurrentUser.Email = email;

			yield return new WaitForSeconds (2f);

			keyboard.HideSuccessMessage ();
			keyboard.SetText ("");
			keyboard.EnableInput ();

			state2 = true;
		}

		private IEnumerator SubmitNick(string nick)
		{
			state2 = false;
			keyboard.ShowSuccessMessage("Никнейм установлен");

			CurrentUser.Nickname = nick;

			yield return new WaitForSeconds(2f);

			keyboard.HideSuccessMessage();
			keyboard.SetText("");
			keyboard.EnableInput();

			state3 = true;
		}

		private IEnumerator SubmitPass(string pass)
		{
			state3 = false;
			keyboard.ShowSuccessMessage("Пароль сохранен");

			CurrentUser.Password = pass;

			yield return new WaitForSeconds(2f);

			keyboard.HideSuccessMessage();
			keyboard.SetText("");
			keyboard.EnableInput();

			keyboard.OnUpdate.RemoveListener(HandleUpdate);
			keyboard.OnSubmit.RemoveListener(HandleSubmit);
			keyboard.OnCancel.RemoveListener(HandleCancel);

			keyboard.Disable();

			HandleCancel();
		}

		private bool ValidateEmail (string text) {
			if (!emailValidator.IsMatch (text)) {
				return false;
			}
			return true;
		}
	}
}