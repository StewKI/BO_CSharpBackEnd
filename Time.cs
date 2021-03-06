
public class Interval{
  private DateTime startTime, endTime;
  private int intesectType;
  private Task? refferedTask;

  public DateTime GetStartTime() => startTime;
  public DateTime GetEndTime() => endTime;
  public TimeSpan GetDuration() => endTime-startTime;
  public int GetIntersectType() => intesectType;
  public Task? GetRefferedTask() => refferedTask;

  public void SetStartTime(DateTime sTime) {
    if(sTime >= endTime) {
      System.Console.WriteLine(sTime + "  --  " + endTime); //DEBUG
      throw new Exception("Error! New StartTime is AFTER current EndTime!");
    }
    else startTime = sTime;
  }
  public void SetEndTime(DateTime eTime) {
    if(eTime <= startTime) {
      System.Console.WriteLine(startTime + "  --  " + eTime); //DEBUG
      throw new Exception("Error! New EndTime is BEFORE current StartTime!");
    }
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
  public void SetRefferedTask(Task rTask) => refferedTask = rTask;

  public Interval() { DateTime now = DateTime.Now; startTime = now; endTime = now; }
  public Interval(DateTime time) { startTime = time; SetEndTime(time); }
  public Interval(DateTime sTime, DateTime eTime) { startTime = sTime; SetEndTime(eTime); }
  public Interval(DateTime sTime, DateTime eTime, int intersectType) { startTime = sTime; SetEndTime(eTime); SetIntersectType(intersectType); }
  public Interval(DateTime sTime, DateTime eTime, Task refferedTask) { startTime = sTime; SetEndTime(eTime); this.refferedTask = refferedTask; }
  public Interval(DateTime sTime, TimeSpan dur) { startTime = sTime; SetDuration(dur); }
  public Interval(Interval toCopy) { startTime = toCopy.GetStartTime(); SetEndTime(toCopy.GetEndTime()); }
  public Interval(DateTime date, int sHour, int sMin, int eHour, int eMin){
    startTime = new DateTime(date.Year, date.Month, date.Day, sHour, sMin, 0);
    SetEndTime(new DateTime(date.Year, date.Month, date.Day, eHour, eMin, 0));
  }

  public override string ToString()
  {
    return /*"("+refferedTask.GetTitle()+")"+*/ startTime.ToShortTimeString() + " <--> " + endTime.ToShortTimeString();
  }

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
        //4th
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

  public static List<Interval> UnifyAll(List<Interval> times){
    if(times.Count() < 2) return times;

    return times;

    for(int i = 0; i<times.Count()-1; i++){
      for(int j = i+1; j<times.Count(); j++){
        if(times[i].Intersect(times[j]) != null);
        //TODO implement...
      }
    }
    return null; //delete this line
  }

  public static List<Interval> SortByStartTime(List<Interval> times, bool asc = true){
    //times.Sort();  TODO research    
    System.Console.WriteLine("Sorting...");
    for(int i = 0; i<times.Count()-1; i++){
      for(int j = i+1; j<times.Count(); j++){
        if(times[i].GetStartTime()>times[j].GetStartTime() == asc){
          var T = times[i];
          times[i] = times[j];
          times[j] = T;
        }
      }
    }
    return times;
  }

  public static List<Interval> Crop(List<Interval> times, Interval toInterval){
    //times = UnifyAll(times);
    List<Interval> R = new List<Interval>();
    for(int i = 0; i<times.Count(); i++){
      Interval? intersect = times[i].Intersect(toInterval);
      if(intersect != null){
        R.Add(intersect);
      }
    }
    return R;
  }

  //TODO check correctness
  private static bool isSorted(List<Interval> times){
    for(int i = 1; i<times.Count(); i++){
      if(times[i-1].GetStartTime() > times[i].GetStartTime()) 
        return false;
    }
    return true;
  }

  //DEBUG
  private static void PrintList(List<Interval> times, string pre){
    foreach(var i in times){
      System.Console.WriteLine(pre + i.ToString());
    }
  }

  public static List<Interval> Invert(List<Interval> times, Interval inInterval){
    if(times.Count()>0){
      times = Crop(times, inInterval);
      if(!isSorted(times)){
        times = SortByStartTime(times);
      }

      PrintList(times,"INVERT* ");
      
      List<Interval> R = new List<Interval>();

      if(inInterval.GetStartTime() < times[0].GetStartTime()){
        R.Add(new Interval(inInterval.GetStartTime(), 
                           times[0].GetStartTime()));
      }

      for(int i = 1; i<times.Count(); i++){
        if(times[i-1].GetEndTime() < times[i].GetStartTime()){
          System.Console.WriteLine(new Interval(times[i-1].GetEndTime(), times[i].GetStartTime())); //DEBUG
          R.Add(new Interval(times[i-1].GetEndTime(), times[i].GetStartTime()));
        }
      }

      if(times[times.Count()-1].GetEndTime() < inInterval.GetEndTime()){
        R.Add(new Interval(times[times.Count()-1].GetEndTime(),
                           inInterval.GetEndTime()));
      }
      return R;
    }
    else{
      return new List<Interval>();
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