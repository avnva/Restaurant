using System;
using Restaurant.repository;

namespace Restaurant.domain.use_case;

public class UserUseCase
{
    private readonly UserRepository _userRepository;

    public UserUseCase(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Execute(User executor, User newUser)
    {
        // Проверка роли текущего пользователя
        if (executor.UserRole.UserRoleName != "ADMIN")
        {
            throw new InvalidOperationException("Only administrators can add new users.");
        }

        // Проверка роли нового пользователя
        if (newUser.UserRole.UserRoleName == "ADMIN" && executor.UserRole.UserRoleName != "ADMIN")
        {
            throw new InvalidOperationException("Only administrators can add new administrators.");
        }

        // Добавление пользователя
        _userRepository.AddUser(newUser);
    }
}