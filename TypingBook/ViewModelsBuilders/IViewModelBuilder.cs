namespace TypingBook.ViewModelsBuilders
{
    public interface IViewModelBuilder<TViewModel>
    {
        TViewModel Build();
        TViewModel Rebuild();
    }
}
