using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Common
{
    internal class Settings
    {
        // Spell magic costs in magic points.
        public const int DragonBreathCost = 1;
        public const int LightningCost = 3;
        public const int ApocalypseCost = 52;
        public const int GateCost = 117;

        // Spell durations in seconds.
        public const float DragonBreathDuration = 2.0f;
        public const float LightningDuration = 5.0f;
        public const float ApocalypseDuration = 0.0f;
        public const float GateDuration = 10.0f;

        // Spell damage amounts in health points.
        public const int DragonBreathDamage = 5;
        public const int LightningDamage = 10;
        public const int ApocalypseDamage = 100;
        public const int GateDamage = 0;

        // General game settings.
        public const int MinLevels = 1;
        public const int MaxLevels = 10;
        public const string GameTitle = "Mortuum";

        // Level size settings.
        public const int TileRows = 8;
        public const int TileColumns = 10;
        public const float LevelHeight = 0.5f;

        // Player values.
        public const int PlayerMaxHealth = 5;
        public const int PlayerMaxShield = 3;
        public const int PlayerMaxMagic = 156;
        public const int PlayerMaxStrength = 10;
        public const float PlayerDyingDuration = 6.0f;
        public const float PlayerHealthRegenDuration = 6.0f;
        public const float PlayerShieldRegenDuration = 3.0f;
        public const float PlayerMoveSpeed = 2.0f;          // In meters per second.
        public const float PlayerTurnSpeed = 200.0f;        // In degrees.
        public const float PlayerWeaponSwingSpeed = 200.0f; // In degrees.

        // Graphics values.
        public const bool SyncWithVTrace = true;
        public const bool FixedTimeStep = true;
        public const int GraphicsWidth = 1024;
        public const int GraphicsHeight = 768;
        public const bool GraphicsFullScreen = false;
        public const SurfaceFormat GraphicsFormat = SurfaceFormat.Bgra5551;

        // Score values.
        public const int ArcherScore = 50;
        public const int GuardScore = 50;
        public const int WizardScore = 100;
        public const int SoulScore = 50;
    }
}
