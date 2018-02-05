# Salary Calculator
�������� ������� ��� ����� �� ��������

## ����� �������
�������� ��������� ��� ������� ������� � ��������.
� ������� ������� ���� ����������� � �� ������ �������� �������� ���������� �������.
�������� ����� �� C# + SQLite. ��� ������� ����� ��������������� �� �����������, �� �� ��������������.

## ��������� �������
### �������:
* �������� ������
* ������ �����������
* ���������� ������� ������� (���� ����������� � �� ������������)
* �������� ����� ��� �������� ������ � ������� �������
* CRUD �������� ��� ������������ (�� �������������)
* ����������� ������ ���� ����������� ��� ����������� (�� �������������)
### �������� �������:
* ����������� ������������� �����������
* ����������� ����������/��������/�������������� �����������
* �������� ���������������� ���������
* ������� �����������, � ������������������ � �������������� ����
* ������ �� "To do List" �������� �� ����������

## ����� �����������
������������ ���������� ������� �� ��������� �����, � ����� ��� �������� ����� ������� Onion ������������, ������ ���� ������� ������ �� ������ �����.
� ����� ���� ��������� �������� ������, ���� ��� ��� ���� � ������������, ������� ��������� �������� ������ � �� ����� ������� ���� ��������� ���� � ��������� 
���������������: ������ � ���� ������, ���������������� ��������� � ���������� �������� ��� ������ � ��������� �������. 
����� ����� �����������: DDD ����������, ������ �����������, �������� ������ �� ����� ������������ �� ������ �����, ������ �������� ���������� ������� �����������,
��� ���������� ������ �����������. ������ ���� ����� ����� ����������� �� �������, ��� ��������� ���������� ����, � ��������� � ������������ 3 ��������� ������������.

��� �������� �������� ������������ � ���� ��� ������ ������ TPH, �.�. ��� ���� ���� ����������� � ����������� ����������.
��� �������� ����������� ��������� ��� ������ ����� ������� ����� Adjacency List. ���� ������ �������� ��� ��������� ��������. ��� ��������� ����������, � �������� ���������, �� �� ��������.

� ������� ����������, ������ ��������� ����� ����� ������ ������ �����. ������ �������� ������ � �������� ��� �������� ��������� ����� EmployeeFactory.
������� �������� ������� ��� ������������ ������ Person, ��� ������ �� ������� Employee, �� Employee ��� ������������ ����������� (�� ���� ������ �.�. ���� ��� ������� � �������), 
��-�� ����� �������� ������ ������� ����� ������� � �����.

## To do List
* ������� todo list �� ���� (Visual studio task list)
* ������ ������� �������� ����� ������� �� ������ Person � ��������� ����� (Single and O/C principles)
* ���������������� �� ���������� ���� ������ � ���������� ������ �����������
* ������� ������ packages
* �������� �������� ������� ���������� � ������
* �������� ����������� ������ �����������
* �������� ����������� ������� ��������� �������� ��� ���� �� �������� (����� ��� ������ ������ �����)
* �������� ���������� ��
* �������������� �����������
* ������������� ������ Equals � GetHashCode ��� Person
* �������� ��������� ����
* �������� Parameter Object ��� ���������� �� �������� ����� ���������� � �������������, ������� � ��� �������� �������� �� ViewModel
* �������������� EmployeeService
* ����������� �������

## ������ ��������� ���������
* ��� �������� ����� ����� ������������ decimal ��� ���������������� ��� Money
* ���� � ��������� ����� ����� �������� �� ��������, �� ��� ����� ����� ������������ ��������� ���������� ����� NodaTime
* ���� ����� ������ ������� � ��, �� ���� �� �� ����� ������� ���������� ����������� � UnitOfWork
* ������ �������� ��� ��� � �������. ������� ���������� �� ��������������� ����������� ������� �������� �� ������ � �������

## �������������� ���������� � ����������

.NET Framework 4.6, C# 7.0, SQLite, Dapper, NUnit, NSubstitute, AutoMapper