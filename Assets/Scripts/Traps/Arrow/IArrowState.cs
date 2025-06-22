public interface IArrowState
{
    void EnterState(Arrow arrow);
    void UpdateState(Arrow arrow);
    void ExitState(Arrow arrow);
}
