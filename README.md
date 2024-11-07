### ПЗПІ-22-6  
### Білецький Данило

# Програмна система зберігання медикаментів

## Огляд

Ця програмна система призначена для автоматизації процесу зберігання медикаментів. Вона використовує IoT-сенсори, мобільний додаток, бекенд-сервер, аналітику та інтуїтивний фронтенд, щоб спростити реєстрацію нових медикаментів для збереження та надати можливість моніторингу та змін умов їх зберігання.

## Основні компоненти

### IoT

- **Сенсори для моніторингу умов зберігання медикаментів**: 
  - Збір даних про температуру та вологість в складському приміщенні, щоб дотримуватись необхідних вимог.
  - Дані використовуються для моніторингу, що здійснюватиметься працівниками, для зменшення ризику псування медикаментів.

### Mobile

- **Перегляд поточних умов зберігання**:
  - Працівники зможуть моніторити в будь який момент часу поточні температуру та вологість в приміщенні.
- **Повідомлення про порушення умов зберігання ліків в приміщенні**:
  - У випадку, коли будь-яке значення вище чи нижче норми - на телефон працівників надійде повідомлення про це.
- **Повідомлення про закінчення строку дії ліків**:
  - У випадку коли строк придатності ліків вичерпається - надійде повідомлення про це

### Backend

- **API для обробки запитів**:
  - Обробка запитів від мобільних і веб-додатків, а також прийом даних від IoT-сенсорів, встановлених в складському примыщенні.
- **Обробка та зберігання даних**:
  - Збереження інформації про медикаменти, зареєстрованих працівників складського приміщення, тощо.

### Frontend

- **Інтерфейс користувача та працівників**:
  - Інтуїтивний веб-інтерфейс для управління організацією (CRUD операції) ліків, праівників, тощо. 
- **Регулювання сенсорів**:
  - Працівники зможуть регулювати мінімальні та максимальні показники рекомендовані для зберігання ліків, порушення яких призведе до отримання працівниками повідомлення про порушення умов зберігання.
 
## Вимоги до встановлення та запуску

1. **IoT-сенсори**: необхідна інтеграція з сенсорами для відслідковування умов зберігання.
2. **Мобільний додаток**: для перегляду поточних умов зберігання та отримання нотифікацій про їх порушення.
3. **Бекенд-сервер**: налаштування API для обробки запитів і зберігання даних.
4. **Фронтенд**: веб-інтерфейс для працівників.
