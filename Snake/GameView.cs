using SFML.Graphics;
using SFML.System;

namespace Snake;

/// <summary>
/// Класс отвечает за отрисовку игры.
/// </summary>
public class GameView
{
    // Настройки отрисовки игры.
    private GameViewSettings _gameViewSettings;

    // Змейка.
    private Snake _snake;

    // Игровое поле.
    private GameBoard _gameBoard;

    // Контроллер еды.
    private FoodController _foodController;

    // Спрайты объектов
    private Sprite _wallSprite;
    private Sprite _backgroundSprite;
    private Sprite _snakeBodySprite;
    private Sprite _snakeHeadSprite;
    private Sprite _foodSprite;


    public GameView(GameViewSettings gameViewSettings, GameBoard gameBoard, Snake snake, FoodController foodController)
    {
        _gameViewSettings = gameViewSettings;
        _snake = snake;
        _foodController = foodController;
        _gameBoard = gameBoard;


        _wallSprite = new Sprite(_gameViewSettings.WallTexture);
        _backgroundSprite = new Sprite(_gameViewSettings.BackgroundTexture);
        _foodSprite = new Sprite(_gameViewSettings.FoodTexture);
        _snakeBodySprite = new Sprite(_gameViewSettings.SnakeBodyTexture);
        _snakeHeadSprite = new Sprite(_gameViewSettings.SnakeHeadTexture);
    }

    /// <summary>
    /// Метод рисует все игровые объекты.
    /// </summary>
    public void DrawGameObjects(RenderWindow window)
    {
        DrawFood(window);
        DrawSnake(window);
    }

    /// <summary>
    /// Метод рисует карту.
    /// </summary>
    public void DrawMap(RenderWindow window)
    {
        // Рисуем фон игры
        window.Draw(_backgroundSprite);

        // Рисуем вертикальные границы карты.
        for (int y = 0; y < _gameBoard.Size.Y; y++)
        {
            // Левая граница 
            _wallSprite.Position = new Vector2f(0, y * _gameViewSettings.CellSize);
            window.Draw(_wallSprite);

            // Правая граница 
            _wallSprite.Position = new Vector2f((_gameBoard.Size.X - 1) * _gameViewSettings.CellSize,
                y * _gameViewSettings.CellSize);
            window.Draw(_wallSprite);
        }

        // Рисуем горизонтальные границы карты.
        for (int x = 0; x < _gameBoard.Size.X; x++)
        {
            // Левая граница 
            _wallSprite.Position = new Vector2f(x * _gameViewSettings.CellSize, 0);
            window.Draw(_wallSprite);

            // Правая граница 
            _wallSprite.Position = new Vector2f(x * _gameViewSettings.CellSize,
                (_gameBoard.Size.Y - 1) * _gameViewSettings.CellSize);
            window.Draw(_wallSprite);
        }
    }

    /// <summary>
    /// Метод рисует еду на карте
    /// </summary>
    private void DrawFood(RenderWindow window)
    {
        _foodSprite.Position = new Vector2f(_foodController.Food.X * _gameViewSettings.CellSize,
            _foodController.Food.Y * _gameViewSettings.CellSize);
        window.Draw(_foodSprite);
    }

    /// <summary>
    /// Метод рисует змейку на карте
    /// </summary>
    private void DrawSnake(RenderWindow window)
    {
        for (int i = 0; i < _snake.GetSize(); i++)
        {
            //Рисуем голову змейки
            if (i == 0)
            {
                // Наши координаты умножаем на размер ячейки
                _snakeHeadSprite.Position = new Vector2f(_snake.GetPoint(i).X * _gameViewSettings.CellSize,
                    _snake.GetPoint(i).Y * _gameViewSettings.CellSize);
                window.Draw(_snakeHeadSprite);
            }
            //Рисуем тело змейки
            else
            {
                // Наши координаты умножаем на размер ячейки
                _snakeBodySprite.Position = new Vector2f(_snake.GetPoint(i).X * _gameViewSettings.CellSize,
                    _snake.GetPoint(i).Y * _gameViewSettings.CellSize);
                window.Draw(_snakeBodySprite);
            }
        }
    }
}