/* OVAJ KOD NE VALJA - OVERCOMPLICATED

public abstract class aTime{

  public aTime(){
  }

  public abstract DateTime GetStartTime();
  public abstract DateTime? GetEndTime();

  public abstract aTime? Intersect(aTime other);

  public static bool Isti(DateTime A, DateTime B){
    if(A.Date.Equals(B.Date) &&
       A.Hour == B.Hour &&
       A.Minute == B.Minute && A.)
  }
}

public class Event: aTime{
  DateTime time;

  public Event(DateTime time){
    this.time = time;
  }

  public override DateTime GetStartTime(){
    return time;
  }

  public override DateTime? GetEndTime()
  {
    return null;
  }

  public override aTime? Intersect(aTime other)
  {
    if(other.GetType().ToString() == "Event"){

    }
    else if(other.GetType().ToString() == "Interval"){

    }
  }
}

public class Interval : aTime{
  DateTime startTime, endTime;

  public Interval(DateTime startTime, DateTime endTime){
    this.startTime = startTime;
    this.endTime = endTime;
  }

  public override DateTime GetStartTime(){
    return startTime;
  }

  public override DateTime? GetEndTime()
  {
    return endTime;
  }

  public override aTime? Intersect(aTime other)
  {
    throw new NotImplementedException();
  }
}
OVERCOMPLICATED*/


public class Interval{
  DateTime startTime, endTime;

  public DateTime GetStartTime() => startTime;
  public DateTime GetEndTime() => endTime;
  public TimeSpan GetDuration() => endTime-startTime;

  public void SetStartTime(DateTime sTime) => startTime = sTime; // TODO error if after endT
  public void SetEndTime(DateTime eTime) => endTime = eTime; // TODO error if before startT
  public void SetDuration(TimeSpan dur) => endTime = startTime+dur;

  public Interval() { startTime = DateTime.Now; endTime = DateTime.Now; }
  public Interval(DateTime time) { startTime = time; endTime = time; }
  public Interval(DateTime sTime, DateTime eTime) { startTime = sTime; endTime = eTime; } // TODO same as above
  public Interval(DateTime sTime, TimeSpan dur) { startTime = sTime; endTime = startTime+dur; }

  public bool NoDuration() => endTime-startTime == new TimeSpan(0);

  public Interval? Intersect(Interval other){
    return null;
    //TODO implement...
  }
}