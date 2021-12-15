
public class Interval{
  DateTime startTime, endTime;
  int intesectType;

  public DateTime GetStartTime() => startTime;
  public DateTime GetEndTime() => endTime;
  public TimeSpan GetDuration() => endTime-startTime;
  public int GetIntersectType() => intesectType;

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
  private void SetIntersectType(int type){
    if(type == 2 || type == 3 || type == 5 || type == 6)
      intesectType = type;
    else
      throw new Exception("Invalid intersect type provided: " + type.ToString());
  }

  public Interval() { DateTime now = DateTime.Now; startTime = now; endTime = now; }
  public Interval(DateTime time) { SetStartTime(time); SetEndTime(time); }
  public Interval(DateTime sTime, DateTime eTime) { SetStartTime(sTime); SetEndTime(eTime); }
  public Interval(DateTime sTime, DateTime eTime, int intersectType) { SetStartTime(sTime); SetEndTime(eTime); SetIntersectType(intersectType); }
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
          return new Interval(other.GetStartTime(), GetEndTime(), 2);
        }
        else{
          //3rd
          return new Interval(other.GetStartTime(), other.GetEndTime(), 3);
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
          return new Interval(GetStartTime(), other.GetEndTime(), 5);
        }
        else{
          //6th
          return new Interval(GetStartTime(), GetEndTime(), 6);
        }
      }
    }
  }

  public Interval? Unify(Interval other){
    Interval? intersect = Intersect(other);
    
    if(intersect == null) return null;

    switch(intersect.GetIntersectType()){
      case 2:
        return new Interval(GetStartTime(), other.GetEndTime(), 2);
        
      case 3:
        return new Interval(GetStartTime(), GetEndTime(), 3);
  
      case 5:
        return new Interval(other.GetStartTime(), GetEndTime(), 5);
        
      case 6:
        return new Interval(other.GetStartTime(), other.GetEndTime(), 6);
        
      default:
        return null;
    }

  }

  public static DateTime ConstructDT(DateTime dateFrom, DateTime timeFrom){
    return new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, timeFrom.Hour, timeFrom.Minute, timeFrom.Second);
  }

  public static DateOnly DateFromDT(DateTime dateFrom){
    return new DateOnly(dateFrom.Year, dateFrom.Month, dateFrom.Day);
  }

  public static TimeOnly TimeFromDT(DateTime timeFrom){
    return new TimeOnly(timeFrom.Hour, timeFrom.Minute, timeFrom.Second, timeFrom.Millisecond);
  }
}