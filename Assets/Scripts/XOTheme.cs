using UnityEngine;

[CreateAssetMenu(fileName = "XOTheme", menuName = "Themes/XOTheme")]
public class XOTheme : ScriptableObject
{
  [SerializeField] public string ThemeName;
  [SerializeField] public Sprite xSprite;
  [SerializeField] public Sprite oSprite;
}
