import sqlite3
from sqlite3 import Error, Connection, Cursor

import os
import pickle

'''
NETWORKS_DATA(
    [name]          [text]  NOT NULL    UNIQUE,
    [author_name]   [text]  NULL,
    [weights]       [text]  NOT NULL,
    [defiats]       [int]   NOT NULL,
    [victories]     [int]   NOT NULL
)
'''
network_table: str = 'NETWORKS_DATA'
db_path: str = 'db.sqlite3'

def _weight_serialization(weights: list) -> bytes:
    return pickle.dumps(weights)

def _weight_deserialization(serialize_weights: bytes) -> list:
    return pickle.loads(serialize_weights)


def _Execute_Query(Query_Creator):
    def Execute(*args, query_args: tuple = None) -> tuple:
        '''
        Get query from Query_Creator function
        Execute request
        return query_result
        '''
        query: str = Query_Creator(*args)                       

        connection: Connection = sqlite3.connect(db_path)
        cursor: Cursor = connection.cursor()
        if query_args:
            query_result: tuple = cursor.execute(query, query_args).fetchall()
        else:
            query_result: tuple = cursor.execute(query).fetchall()
        connection.commit()
        connection.close()

        return query_result
    return Execute

@_Execute_Query
def _select_(table_name: str, request_condition: str='') -> str:
    '''
    request_condition mast be like 'WHERE ...' or ''
    '''
    query = 'SELECT * FROM {0} {1}'.format(table_name, request_condition)
    return query

@_Execute_Query
def _insert_into_(table_name: str, *col_names: str) -> str:
    columns: str = '('
    values: str = '('

    for name in col_names:
        columns += name+', '
        values += '?, '

    columns = columns[0:-2]+')'
    values = values[0:-2]+')'

    query: str = 'INSERT INTO {0} {1} VALUES {2}'.format(table_name, columns, values)

    return query

@_Execute_Query
def _update_(table_name: str, request_condition: str = '', *update_columns: str) -> str:
    '''
    request_condition mast be like 'WHERE ...' or ''
    '''
    set_sting: str = ''

    for name in update_columns:
        set_sting += '{0} = ?, '.format(name)
    set_sting = set_sting[0:-2]

    query = 'UPDATE {0} SET {1} {2}'.format(table_name, set_sting, request_condition)

    return query

@_Execute_Query
def _delete_(table_name: str, request_condition: str) -> str:
    '''
    request_condition mast be like 'WHERE ...' or ''
    example: _delete_('table', 'WHERE name = ?', query_args=('name',))
    '''
    query = 'DELETE FROM {0} {1}'.format(table_name, request_condition)

    return query

def DB_TESTS():
    # INSERT TEST
    print()
    print('TEST 1 - INSERT ')
    _insert_into_(network_table, 'name', 'author_name', 'weights', query_args=('Test', 'Test Author', _weight_serialization([[1.1],[2.2]])))
    _insert_into_(network_table, 'name', 'author_name', 'weights', query_args=('Test2', 'Test Author2', _weight_serialization([[1.3],[2.4]])))

    # SELECT TEST 1
    select = _select_(network_table)
    for line in select:
        format_line = 'Name = {0}, Author Name = {1}, Weights = {2}, Defiats = {3}, Victories = {4}'.format(
            line[0], line[1], _weight_deserialization(line[2]), line[3], line[4]
        )
        print(format_line)

    # UPDATE TEST
    print()
    print('TEST 2 - UPDATE ')
    _update_(network_table, 'WHERE name = ?', 'name', 'author_name', query_args=('Test1', 'Test Author1', 'Test'))

    # SELECT TEST 2
    select = _select_(network_table, 'WHERE name = ?', query_args=('Test1',))
    for line in select:
        format_line = 'Name = {0}, Author Name = {1}, Weights = {2}, Defiats = {3}, Victories = {4}'.format(
            line[0], line[1], _weight_deserialization(line[2]), line[3], line[4]
        )
        print(format_line)
    

    # DELETE TEST 1
    print()
    print('TEST 3 - DELETE ')
    _delete_(network_table, 'WHERE name = ?', query_args=('Test2',))

    # SELECT TEST 3
    select = _select_(network_table)
    for line in select:
        format_line = 'Name = {0}, Author Name = {1}, Weights = {2}, Defiats = {3}, Victories = {4}'.format(
            line[0], line[1], _weight_deserialization(line[2]), line[3], line[4]
        )
        print(format_line)

    # DELETE TEST 2
    print()
    print('TEST 4 - DELETE ALL ')
    _delete_(network_table, 'WHERE ? = ?', query_args=('1','1'))

    # SELECT TEST 4
    select = _select_(network_table)
    for line in select:
        format_line = 'Name = {0}, Author Name = {1}, Weights = {2}, Defiats = {3}, Victories = {4}'.format(
            line[0], line[1], _weight_deserialization(line[2]), line[3], line[4]
        )
        print(format_line)
    else:
        print('Table is Empty!')

if __name__ == "__main__":
    DB_TESTS()