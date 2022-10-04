using UnityEngine;

namespace TMPro.Examples
{
	public class TMP_TextEventCheck : MonoBehaviour
	{
		public TMP_TextEventHandler TextEventHandler;

		private void OnEnable()
		{
			if (TextEventHandler != null)
			{
				TextEventHandler.onCharacterSelection.AddListener(OnCharacterSelection);
				TextEventHandler.onSpriteSelection.AddListener(OnSpriteSelection);
				TextEventHandler.onWordSelection.AddListener(OnWordSelection);
				TextEventHandler.onLineSelection.AddListener(OnLineSelection);
				TextEventHandler.onLinkSelection.AddListener(OnLinkSelection);
			}
		}

		private void OnDisable()
		{
			if (TextEventHandler != null)
			{
				TextEventHandler.onCharacterSelection.RemoveListener(OnCharacterSelection);
				TextEventHandler.onSpriteSelection.RemoveListener(OnSpriteSelection);
				TextEventHandler.onWordSelection.RemoveListener(OnWordSelection);
				TextEventHandler.onLineSelection.RemoveListener(OnLineSelection);
				TextEventHandler.onLinkSelection.RemoveListener(OnLinkSelection);
			}
		}

		private void OnCharacterSelection(char c, int index)
		{
			UnityEngine.Debug.Log("Character [" + c.ToString() + "] at Index: " + index + " has been selected.");
		}

		private void OnSpriteSelection(char c, int index)
		{
			UnityEngine.Debug.Log("Sprite [" + c.ToString() + "] at Index: " + index + " has been selected.");
		}

		private void OnWordSelection(string word, int firstCharacterIndex, int length)
		{
			UnityEngine.Debug.Log("Word [" + word + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
		}

		private void OnLineSelection(string lineText, int firstCharacterIndex, int length)
		{
			UnityEngine.Debug.Log("Line [" + lineText + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
		}

		private void OnLinkSelection(string linkID, string linkText, int linkIndex)
		{
			UnityEngine.Debug.Log("Link Index: " + linkIndex + " with ID [" + linkID + "] and Text \"" + linkText + "\" has been selected.");
		}
	}
}
