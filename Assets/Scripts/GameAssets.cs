using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAssets : MonoBehaviour
{
    public Transform pfDamagePopup;
    public Transform pfEnhancementPopup;
    public Transform pfMashedPotatoes;
    public Transform pfCottonCandyPlanet;
    public Transform pfBlueGreenPlanet;
    public Transform pfGreenPlanet;
    public Transform pfGreenPurplePlanet;
    public Transform pfGreenRedPlanet;
    public Transform pfRedBluePlanet;
    public Transform pfRedPlanet;
    public Transform pfYellowBluePlanet;
    public Transform pfYellowPlanet;
    public Transform pfYellowRedPlanet;
    public Transform pfLootBag;
    public Transform pfLootBagText;
    public Transform pfComet1;
    public Transform pfAsteroid1;
    public Transform pfPrecisionTarget;
    public Sprite enhancementTypePotency;
    public Sprite enhancementTypeFocus;
    public Sprite enhancementTypeTimed;
    public Sprite enhancementTypePrecision;
    public Sprite enhancementQualityLegendary;
    public Sprite enhancementQualityEpic;
    public Sprite enhancementQualityRare;
    public Sprite enhancementQualityUncommon;
    public Sprite enhancementQualityCommon;

    private static GameAssets _i;

    public static GameAssets i 
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return _i;
        }
    }

}
