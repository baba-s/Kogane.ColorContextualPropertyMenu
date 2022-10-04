using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class ColorContextualPropertyMenu
    {
        static ColorContextualPropertyMenu()
        {
            EditorApplication.contextualPropertyMenu -= OnMenu;
            EditorApplication.contextualPropertyMenu += OnMenu;
        }

        private static void OnMenu( GenericMenu menu, SerializedProperty property )
        {
            if ( property.propertyType != SerializedPropertyType.Color ) return;

            var color   = property.colorValue;
            var color32 = ( Color32 )color;
            var rgb     = ColorUtility.ToHtmlStringRGB( color );
            var rgba    = ColorUtility.ToHtmlStringRGBA( color );

            AddItem( menu, $"new Color( {color.r}, {color.g}, {color.b}, {color.a} )" );
            AddItem( menu, $"new Color32( {color32.r}, {color32.g}, {color32.b}, {color32.a} )" );
            AddItem( menu, $"#{rgb}" );
            AddItem( menu, $"#{rgba}" );
        }

        private static void AddItem( GenericMenu menu, string copiedText )
        {
            menu.AddItem
            (
                content: new( $"Copy '{copiedText}'" ),
                on: false,
                func: () =>
                {
                    EditorGUIUtility.systemCopyBuffer = copiedText;
                    Debug.Log( $"Copied! '{copiedText}'" );
                }
            );
        }
    }
}