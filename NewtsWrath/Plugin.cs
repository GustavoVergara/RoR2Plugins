using BepInEx;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using R2API.Utils;

namespace NewtsWrath
{

    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin("com.mush.newtswrath", "Newt's Wrath", "1.0.2")]
    public class Plugin : BaseUnityPlugin
    {
        readonly ArtifactDef NewtsWrath = ScriptableObject.CreateInstance<ArtifactDef>();
        bool NewtsWrathIsEnabled => RunArtifactManager.instance.IsArtifactEnabled(NewtsWrath.artifactIndex);

        public void Awake() 
        {
            this.PrepareArtifact();

            On.RoR2.Run.BuildDropTable += Run_BuildDropTable;
        }

        private void PrepareArtifact()
        {
            NewtsWrath.nameToken = "Newt's Wrath";
            NewtsWrath.descriptionToken = "Every item is a lunar item.";
            NewtsWrath.smallIconDeselectedSprite = Sprite.Create(
                Utils.LoadTexture2D(Properties.Resources.ArtifactDeselected),
                new Rect(0, 0, 128, 128),
                new Vector2(0.5f, 0.5f)
            );
            NewtsWrath.smallIconSelectedSprite = Sprite.Create(
                Utils.LoadTexture2D(Properties.Resources.ArtifactSelected), 
                new Rect(0, 0, 128, 128),
                new Vector2(0.5f, 0.5f)
            );

            ArtifactCatalog.getAdditionalEntries += (list) =>
            {
                list.Add(NewtsWrath);
            };
        }

        private void Run_BuildDropTable(On.RoR2.Run.orig_BuildDropTable orig, Run self)
        {
            orig(self);
            if (NewtsWrathIsEnabled)
            {
                self.SetFieldValue("availableTier1DropList", self.availableLunarDropList);
                self.SetFieldValue("availableTier2DropList", self.availableLunarDropList);
                self.SetFieldValue("availableTier3DropList", self.availableLunarDropList);
                self.SetFieldValue("availableNormalEquipmentDropList", self.availableLunarEquipmentDropList);
            }
        }

    }

}
