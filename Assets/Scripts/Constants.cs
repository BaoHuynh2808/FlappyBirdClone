public static class Constants
{
    public static readonly float BlockMoveSpeed = 1f;

    public static readonly float FloorOffset = 1.97f;

    public static readonly float BlockOffSet = 0.7f;

    public static readonly string SceneMenu = "GameMenu";

    public static readonly string ScenePlay = "GamePlay";

    public static class Animation
    {
        public static readonly string BirdFly = "Fly";
        public static readonly string BirdIdle = "Idle";
        public static readonly string BirdKo = "Ko";
        public static readonly string BirdHurt = "Hurt";
    }

    public static class GameEvent
    {
        public static readonly string OnUpdateScoreText = "OnUpdateScoreText";
    }
}
