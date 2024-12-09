using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ErmisChat.SampleProject.Configs
{
    public interface IEmojiConfig
    {
        TMP_SpriteAsset TMPSpriteAsset { get; }
        Texture2D EmojisAtlasTexture { get; }
        IEnumerable<Sprite> ReactionSprites { get; }
        IEnumerable<Sprite> AllSprites { get; }

        void LoadEmojisSprites();
    }
}