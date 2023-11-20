using ImGuiNET;

namespace Fizzle.Managers
{
    public class GUIManager
    {
        private bool guiActive = true;

        public void DrawGUI(string name, string labelName, bool fileOptions, params string[] output)
        {
            if (guiActive)
            {

                if (fileOptions)
                {
                    ImGui.Begin(name, ref guiActive, ImGuiWindowFlags.MenuBar);
                    AddFileOptions();
                }
                else
                    ImGui.Begin(name, ref guiActive);


                AddLabel(labelName);

                AddScrollBar($"{labelName}bar", new System.Numerics.Vector2(125, 200),output);

                ImGui.End();
            }
        }

        private void AddFileOptions()
        {
            if (ImGui.BeginMenuBar())
            {

                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("Open..", "Ctrl+O")) ;
                    if (ImGui.MenuItem("Save", "Ctrl+S")) ;
                    if (ImGui.MenuItem("Close", "Ctrl+W"))
                        guiActive = false;

                    ImGui.EndMenu();

                }
                ImGui.EndMenuBar();
            }
        }
        private void AddScrollBar(string barName, System.Numerics.Vector2 size, params string[] text)
        {
            ImGui.BeginChild(barName, size, true);
            foreach (var line in text)
                ImGui.Text(line);
            ImGui.EndChild();
        }
        private void AddLabel(string name)
        {
            // RGBA
            ImGui.TextColored(new Vector4(0, 1, 0, 1f).ToNumerics(), name);
        }
    }
}
