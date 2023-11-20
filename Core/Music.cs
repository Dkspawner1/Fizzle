using Microsoft.Xna.Framework.Media;

namespace Fizzle;

internal class Music
{
    public Song Song { get; }
    public Music(Song song, bool repeat, float volume)
    {
        Song = song;
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Volume = volume;
    }
    public void Update()
    {
        // if (MediaPlayer.State is MediaState.Playing) MediaPlayer.Play(Song);
        // else if (MediaPlayer.State is MediaState.Paused) MediaPlayer.Resume();

    }
    private void stopSong(bool rewind = true)
    {
        if (rewind) MediaPlayer.Stop();
        else MediaPlayer.Pause();
    }
    private void unpause() => MediaPlayer.Resume();

}