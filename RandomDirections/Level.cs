using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDirections
{
    public enum CutDirection
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
        UPLEFT = 4,
        UPRIGHT = 5,
        DOWNLEFT = 6,
        DOWNRIGHT = 7,
        ANY = 8
    }

    public enum LineLayer
    {
        BOTTOM = 0,
        MIDDLE = 1,
        TOP = 2
    }

    public enum LineIndex
    {
        LEFT = 0,
        LEFTMIDDLE = 1,
        MIDDLE = 2,
        RIGHTMIDDLE = 3,
        RIGHT = 4
    }

    public enum NoteColor
    {
        RED = 0,
        BLUE = 1,
        BOMB = 3
    }

    public enum ObstacleType
    {
        WALL = 0
    }

    public class Event
    {
        public double _time { get; set; }
        public int _type { get; set; }
        public int _value { get; set; }
    }

    public class Note
    {
        public double _time { get; set; }
        public LineIndex _lineIndex { get; set; }
        public LineLayer _lineLayer { get; set; }
        public NoteColor _type { get; set; }
        public CutDirection _cutDirection { get; set; }
    }

    public class Obstacle
    {
        public double _time { get; set; }
        public LineIndex _lineIndex { get; set; }
        public ObstacleType _type { get; set; }
        public double _duration { get; set; }
        public int _width { get; set; }
    }

    public class Level
    {
        public string _version { get; set; }
        public double _beatsPerMinute { get; set; }
        public int _beatsPerBar { get; set; }
        public double _noteJumpSpeed { get; set; }
        public double _shuffle { get; set; }
        public double _shufflePeriod { get; set; }
        public List<Event> _events { get; set; }
        public List<Note> _notes { get; set; }
        public List<Obstacle> _obstacles { get; set; }
    }
}
