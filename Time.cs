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

  public void SetStartTime(DateTime sTime) {
    if(sTime >= endTime) 
      throw new Exception("Error! New StartTime is AFTER current EndTime!");
    else startTime = sTime;
  }
  public void SetEndTime(DateTime eTime) {
    if(eTime <= startTime) 
      throw new Exception("Error! New EndTime is BEFORE current StartTime!");
    else endTime = eTime;
  }
  public void SetDuration(TimeSpan dur) {
    if(dur < new TimeSpan(0))
      throw new Exception("Error! New Duration is NEGATIVE!");
    else endTime = startTime+dur;
  }

  public Interval() { startTime = DateTime.Now; endTime = DateTime.Now; }
  public Interval(DateTime time) { SetStartTime(time); SetEndTime(time); }
  public Interval(DateTime sTime, DateTime eTime) { SetStartTime(sTime); SetEndTime(eTime); }
  public Interval(DateTime sTime, TimeSpan dur) { SetStartTime(sTime); SetDuration(dur); }
  public Interval(Interval toCopy) { SetStartTime(toCopy.GetStartTime()); SetEndTime(toCopy.GetEndTime()); }

  public bool NoDuration() => endTime-startTime == new TimeSpan(0);

  public Interval? Intersect(Interval other){
    if(GetStartTime() < other.GetStartTime()){
      if(GetEndTime() < other.GetStartTime()){
        //1st
        return null;
      }
      else{
        if(GetEndTime() < other.GetEndTime()){
          //2nd
          return new Interval(other.GetStartTime(), GetEndTime());
        }
        else{
          //3rd
          return new Interval(other);
        }
      }
    }
    else{
      if(other.GetEndTime() < GetStartTime()){
        //4rd
        return null;
      }
      else{
        if(other.GetEndTime() < GetEndTime()){
          //5th
          return new Interval(GetStartTime(), other.GetEndTime());
        }
        else{
          //6th
          return new Interval(GetStartTime(), GetEndTime());
        }
      }
    }
  }
}