using System;
using System.Collections.Generic;

public static class SkillsModule
{
    public static SkillsFocus skillsFocus = SkillsFocus.Title;
    public static bool viewingSkillDescription = false;
    private static int selectedSkillIndex;
    private static int level = 4;
    private static int xp = 10;
    private static int remainingPoints = 3;
    private static int hackLevel = 2;
    private static int craftLevel = 0;
    private static int survivalLevel = 2;

    public static void DrawSkills() { /* skopiuj DrawSkills z Program.cs */ }
    public static void HandleSkillsInput(ConsoleKeyInfo key, List<Tab> tabs, ref int currentTabIndex) { /* skopiuj */ }
    public static void DrawSkillDescription() { /* skopiuj */ }
}