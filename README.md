IgushArray
==========

C# implementation of IgushArray data structure
(autor made it on C++)
May be used instead of List in cases with many insertion/removing operations

Declarations of original IgushArray realization
-----------------------------------------------

http://igushev.ru/papers/igusharray/ - not works now, but you can try to google it!
Эдуард Игушев
IgushArray (быстрый массив)

Licence of original IgushArray realization
------------------------------------------

Загрузка. Гарантия и лицензия

Реализация поставляется “как есть”.
Любое некоммерческое и коммерческое использование разрешено.
Сохранение оригинального имени и ссылку на источник обязательно.
Любой отзыв желателен :-)

Algorithmic complexity characteristics
--------------------------------------

- Access by index - O(1)
- Insertion at index - 0(sqrt(n)) - better than List (O(n))
- Insertion at end - 0(1)
- Removing at index - 0(sqrt(n)) - better than List (O(n))
- Iteration by all elements of array - O(n)

Disadvantages
-------------

- Parameter m = sqrt(n) mast be settet manualy
- Parameter m can't be changed after structure creation, although structure is reserve memory dynamic, it can't change subblock size
- Iteration/random access is slow by comparision with List, although they are equivalent by algoritmic complexity
- Starts to overtake List on insertions only after ~5000 items
